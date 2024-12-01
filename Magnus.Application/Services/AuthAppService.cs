using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Magnus.Application.Services;

public class AuthAppService(
    IConfiguration configuration,
    IUnitOfWork unitOfWork) : IAuthAppService
{
    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var userDb = await unitOfWork.Users.GetUserByEmailAsync(request.Email, cancellationToken);
        if (userDb is null)
            throw new AuthenticationException("Usuário ou senha inválidos");
        if(!userDb.Password.Equals(request.Password))
            throw new AuthenticationException("Usuário ou senha inválidos");
        var token = CreateToken(userDb);
        var refreshToken = CreateRefreshToken(userDb);
        return new LoginResponse(token, refreshToken);
    }

    public async Task<LoginResponse> RefreshLoginAsync(RefreshLoginRequest request, CancellationToken cancellationToken)
    {
        var email = await ValidateRefreshTokenAsync(request.RefreshToken);
        if(string.IsNullOrEmpty(email))
            throw new AuthenticationException("Refresh token inválido");
        
        var userDb = await unitOfWork.Users.GetUserByEmailAsync(email, cancellationToken);
        if (userDb is null)
            throw new AuthenticationException("Usuário ou senha inválidos");
        
        var token = CreateToken(userDb);
        var refreshToken = CreateRefreshToken(userDb);
        return new LoginResponse(token, refreshToken);
    }

    private string CreateToken(User user)
    {
        var secretKey = GetSecretKey();
        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Role, nameof(user.UserType)),
                new Claim(ClaimTypes.Email, user.Email.Address),
                new Claim("UserId", user.Id.ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Nbf,
                    ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Iat,
                    ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);
        return tokenHandler.WriteToken(token);
    }

    private string CreateRefreshToken(User user)
    {
        var secretKey = GetSecretKey();
        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Email, user.Email.Address),
            ]),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);
        return tokenHandler.WriteToken(token);
    }

    public async Task<string?> ValidateRefreshTokenAsync(string refreshToken)
    {
        var secretKey = GetSecretKey();
        var handler = new JsonWebTokenHandler();
        var result = await handler.ValidateTokenAsync(refreshToken, new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        });

        return !result.IsValid ? null : result.Claims[Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email].ToString();
    }

   

    private byte[] GetSecretKey()
    {
        var key = configuration["Jwt:SecretKey"];
        if (key is null)
            throw new AuthenticationException("key não encontrada");
        
        return Encoding.UTF8.GetBytes(key);
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
}