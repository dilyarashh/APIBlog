namespace socials.SupportiveServices.Password;

public class HashPassword
{
    public string HashingPassword(string password)
    {
        int workFactor = 12; 
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, workFactor);
        return hashedPassword;
    }

    public static bool VerifyPassword(string providedPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}