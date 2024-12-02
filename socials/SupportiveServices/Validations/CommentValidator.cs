namespace socials.SupportiveServices.Validations;

public class CommentValidator
{
    public static bool ValidateComment(string title)
    {
        return title.Length >= 1 && title.Length <= 100;
    }
}