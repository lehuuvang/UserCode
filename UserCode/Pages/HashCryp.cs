namespace UserCode.Pages
{
    public class HashCryp
    {
        private static string GetRandomSalt()
        {
            String satl = BCrypt.Net.BCrypt.GenerateSalt(12);
            return satl;
        }
        public static string HashPassword(String password) {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public static bool ValidatePassword(String password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
