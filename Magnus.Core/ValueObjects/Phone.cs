using System.Text.RegularExpressions;
using Magnus.Core.Enumerators;

namespace Magnus.Core.ValueObjects;

public class Phone
{
    public string Number { get; }
    public PhoneType PhoneType { get; }
    public Phone(string number, PhoneType phoneType)
    {
        if (string.IsNullOrEmpty(number) || number.Length < 10 || number.Length > 11)
            throw new ArgumentException("Número inválido");
        if (!IsValidPhoneNumber(number, phoneType))
            throw new ArgumentException("Número inválido");
        Number = number;
        PhoneType = phoneType;
    }
    private bool IsValidPhoneNumber(string number, PhoneType phoneType)
    {
        switch (phoneType)
        {
            case PhoneType.Mobile:
                return IsValidMobilePhone(number);
            case PhoneType.Fixed:
                return IsValidFixedPhone(number);
            default:
                return false;
        }
    }
    
    private bool IsValidMobilePhone(string number)
    {
        var regex = new Regex(@"^\(\d{2}\)\s9\d{4}-\d{4}$");
        return regex.IsMatch(number);
    }
    
    private bool IsValidFixedPhone(string number)
    {
        var regex = new Regex(@"^\(\d{2}\)\s\d{4}-\d{4}$");
        return regex.IsMatch(number);
    }

    public override string ToString() => Number;
}