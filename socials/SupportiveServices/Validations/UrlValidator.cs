using System.Text.RegularExpressions;

namespace socials.SupportiveServices.Validations;

public class UrlValidator
{
    private static readonly Regex urlRegex = new Regex(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", RegexOptions.Compiled);

    public static bool IsValidUrl(string url)
    {
        return urlRegex.IsMatch(url);
    }
}