using System.Text.RegularExpressions;

namespace Magnus.Application.Dtos.Requests.Validators;

public static class DocumentValidator
{
    public static bool IsValidDocument(string document)
    {
        return document.Length == 11 ? IsValidCpf(document) : IsValidCnpj(document);
    }

    private static bool IsValidCpf(string cpf)
    {
        return Regex.IsMatch(cpf, @"^\d{11}$");
    }

    private static bool IsValidCnpj(string cnpj)
    {
        return Regex.IsMatch(cnpj, @"^\d{14}$");
    }
}