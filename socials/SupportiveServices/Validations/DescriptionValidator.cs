namespace socials.SupportiveServices.Validations;

public class DescriptionValidator
{
    public static bool ValidateDescription(string description)
    {
        return description.Length >= 1 && description.Length <= 3000;
    }
}