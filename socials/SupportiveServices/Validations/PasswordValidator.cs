using System.Text.RegularExpressions;

namespace socials.SupportiveServices.Validations;

public class PasswordValidator
{
    public static bool IsValidPassword(string password)
    {
        return !string.IsNullOrEmpty(password) && password.Length >= 8;
    }
}