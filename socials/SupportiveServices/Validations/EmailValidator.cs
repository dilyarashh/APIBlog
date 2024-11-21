using System.Text.RegularExpressions;

namespace socials.SupportiveServices.Validations;

public class EmailValidator
{
    public static bool ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}