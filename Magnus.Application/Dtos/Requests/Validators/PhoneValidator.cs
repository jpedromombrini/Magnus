using System.Text.RegularExpressions;
using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests.Validators;

public static class PhoneValidator
{
    public static bool IsValidPhoneNumber(string number, PhoneType phoneType)
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
    
    private static bool IsValidMobilePhone(string number)
    {
        var regex = new Regex(@"^\d{11}$");
        return regex.IsMatch(number);
    }
    
    private static bool IsValidFixedPhone(string number)
    {
        var regex = new Regex(@"^\d{10}$");
        return regex.IsMatch(number);
    }
}