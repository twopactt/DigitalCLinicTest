namespace DigitalCLinicTest.Helpers
{
    public class EmailValidator
    {
        public static (bool IsValid, string Message) ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, "Email не может быть пустым");

            if (email.Length > 255)
                return (false, "Email слишком длинный");

            return (true, "Email валиден");
        }
    }
}
