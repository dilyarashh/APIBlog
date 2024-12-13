namespace socials.SupportiveServices.Validations;

public class GroupNameValidator
{
    public static bool ValidateName(string name)
    {
        return name.Length >= 1 && name.Length <= 50;
    }
}