using System.Text.RegularExpressions;

namespace Magnus.Core.ValueObjects;

public class Document 
{
    public string Value { get; private set; }

    public Document(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return;
        value = value.Replace(".", "").Replace("-", "").Replace("/", "").Trim(); 

        if (!IsValid(value))
            throw new ArgumentException("Documento inválido.");

        Value = value;
    }
    
    private bool IsValid(string value)
    {
        if (value.Length == 11) 
        {
            return IsCpfValid(value);
        }
        else if (value.Length == 14) 
        {
            return IsCnpjValid(value);
        }
        else
        {
            return false;
        }
    }
    private bool IsCpfValid(string cpf)
    {
        if (!Regex.IsMatch(cpf, @"^\d{11}$"))
            return false;
        
        if (new string(cpf[0], 11) == cpf)
            return false;

        // Validar o CPF utilizando o algoritmo de verificação
        int[] multiplicadores1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicadores2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        var soma1 = 0;
        for (var i = 0; i < 9; i++)
        {
            soma1 += (cpf[i] - '0') * multiplicadores1[i];
        }

        var resto1 = soma1 % 11;
        var digito1 = (resto1 < 2) ? 0 : 11 - resto1;

        var soma2 = 0;
        for (var i = 0; i < 10; i++)
        {
            soma2 += (cpf[i] - '0') * multiplicadores2[i];
        }

        var resto2 = soma2 % 11;
        var digito2 = (resto2 < 2) ? 0 : 11 - resto2;

        return cpf[9] == (char)(digito1 + '0') && cpf[10] == (char)(digito2 + '0');
    }
    
    private bool IsCnpjValid(string cnpj)
    {
        if (!Regex.IsMatch(cnpj, @"^\d{14}$"))
            return false;
        
        if (new string(cnpj[0], 14) == cnpj)
            return false;
        
        int[] multiplicadores1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicadores2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        var soma1 = 0;
        for (var i = 0; i < 12; i++)
        {
            soma1 += (cnpj[i] - '0') * multiplicadores1[i];
        }

        var resto1 = soma1 % 11;
        var digito1 = (resto1 < 2) ? 0 : 11 - resto1;

        var soma2 = 0;
        for (var i = 0; i < 13; i++)
        {
            soma2 += (cnpj[i] - '0') * multiplicadores2[i];
        }

        var resto2 = soma2 % 11;
        var digito2 = (resto2 < 2) ? 0 : 11 - resto2;

        return cnpj[12] == (char)(digito1 + '0') && cnpj[13] == (char)(digito2 + '0');
    }

    public override string ToString()
    {
        return Value.Length switch
        {
            11 => long.Parse(Value).ToString(@"000\.000\.000\-00"),
            14 => long.Parse(Value).ToString(@"00\.000\.000\/0000\-00"),
            _ => Value
        };
    }
}