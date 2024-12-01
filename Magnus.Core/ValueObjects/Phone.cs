using System.Text.RegularExpressions;
using Magnus.Core.Enumerators;

namespace Magnus.Core.ValueObjects;

public class Phone
{
    public string Number { get; }
    public Phone(string number)
    {
        if (string.IsNullOrEmpty(number) || number.Length < 10 || number.Length > 11)
            throw new ArgumentException("Número inválido");
        if (!IsValidPhoneNumber(number))
            throw new ArgumentException("Número inválido");
        Number = number;
    }
    private bool IsValidPhoneNumber(string number)
    {
        var regexMobile = new Regex(@"^\(\d{2}\)\s9\d{4}-\d{4}$");
        if (regexMobile.IsMatch(number))
            return true;
        var regexFixed = new Regex(@"^\(\d{2}\)\s\d{4}-\d{4}$");
        if(regexFixed.IsMatch(number))
            return true;
        return false;
    }

    public override string ToString() => Number;
}