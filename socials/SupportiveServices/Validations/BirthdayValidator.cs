namespace socials.SupportiveServices.Validations;

public class BirthdayValidator
{
    public static bool ValidateBirthday(DateTime? birthday)
    {
        DateTime minDate = new DateTime(1900, 1, 1); 
        DateTime maxDate = DateTime.Now;

        return birthday >= minDate && birthday <= maxDate;
    }
}