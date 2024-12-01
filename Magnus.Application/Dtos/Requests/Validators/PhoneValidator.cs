using System.Text.RegularExpressions;
using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests.Validators;

public static class PhoneValidator
{
    public static bool IsValidPhoneNumber(string number)
    {
        var regexMobile = new Regex(@"^\(\d{2}\)\s9\d{4}-\d{4}$");
        if (regexMobile.IsMatch(number))
            return true;
        var regexFixed = new Regex(@"^\(\d{2}\)\s\d{4}-\d{4}$");
        if(regexFixed.IsMatch(number))
            return true;
        return false;
    }
}