namespace Mokka.Models;

public static class Passwords
{
    /// <summary>Mokka rule: at least 8 characters and at least one digit.</summary>
    public static bool IsStrong(string password) => password.Length >= 8 && password.Any(char.IsDigit);
}
