using System.Text.RegularExpressions;

namespace socials.SupportiveServices.Validations;

public class PhoneValidator
{
    public static bool IsValidePhoneNumber(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return false;
        }

        if (phone.Length != 11 || !phone.All(char.IsDigit))
        {
            return false;
        }

        return true;
    }
}