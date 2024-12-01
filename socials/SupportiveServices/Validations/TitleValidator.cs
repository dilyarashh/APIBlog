namespace socials.SupportiveServices.Validations;

public class TitleValidator
{
    public static bool ValidateTitle(string title)
    {
        return title.Length >= 1 && title.Length <= 100;
    }
}
