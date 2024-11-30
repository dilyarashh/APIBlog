using System.Text.RegularExpressions;
using socials.SupportiveServices.Validations;

public static class NameValidator
{
    public static bool IsValidFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return false;
        }

        string[] nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (nameParts.Length < 2)
        {
            return false;
        }

        foreach (string namePart in nameParts)
        {
            if (!IsValidNamePart(namePart))
            {
                return false;
            }
        }

        return true;
    }
    private static bool IsValidNamePart(string namePart)
    {
        if (namePart.Length < 2 || !char.IsUpper(namePart[0]))
        {
            return false;
        }

        return Regex.IsMatch(namePart, "^[A-ZА-ЯЁ][a-zа-яё]+$"); 
    }
}
