using System.Text.RegularExpressions;
using Magnus.Core.Exceptions;

namespace Magnus.Core.ValueObjects;

public class Email 
{
    public string Address { get; }
    public Email(string address)
    {
        if (string.IsNullOrEmpty(address) || address.Length < 5)
            throw new InvalidEmailException("E-mail inválido");

        Address = address.ToLower().Trim();
        const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        if (!Regex.IsMatch(address, pattern))
            throw new InvalidEmailException("E-mail inválido");
    }
    public override string ToString() => Address;
}