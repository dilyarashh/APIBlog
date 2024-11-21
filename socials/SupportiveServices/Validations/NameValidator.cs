using System.Text.RegularExpressions;
using socials.SupportiveServices.Validations;

public class NameValidator
{
    public static bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        if (name.Length < 2)
        {
            return false;
        }

        if (!char.IsLetter(name[0]))
        {
            return false;
        }

        if (name.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)))
        {
            return false;
        }

        return true;
    }
}
