using System.Text.RegularExpressions;

namespace Magnus.Core.ValueObjects;

public partial class Phone
{
    public Phone(string number)
    {
        if (string.IsNullOrEmpty(number) || !IsValidPhoneNumber(number))
            throw new ArgumentException("Número inválido");
        Number = number;
    }

    public string Number { get; }

    private static bool IsValidPhoneNumber(string number)
    {
        var regexMobile = RegexMobilePhone();
        if (regexMobile.IsMatch(number))
            return true;
        var regexFixed = RegexFixedPhone();
        return regexFixed.IsMatch(number);
    }

    public override string ToString()
    {
        return Number;
    }

    [GeneratedRegex(@"^\(\d{2}\)\s9\d{4}-\d{4}$")]
    private static partial Regex RegexMobilePhone();

    [GeneratedRegex(@"^\(\d{2}\)\s\d{4}-\d{4}$")]
    private static partial Regex RegexFixedPhone();
}