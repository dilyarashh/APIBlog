namespace socials.SupportiveServices.Validations;

public class GroupDescriptionValidator
{
    public static bool ValidateDescription(string description)
    {
        return description.Length >= 1 && description.Length <= 300;
    }
}