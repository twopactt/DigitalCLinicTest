using System.Text.RegularExpressions;

namespace DigitalCLinicTest.Helpers
{
    public class PasswordValidator
    {
        public static (bool IsValid, string Message) ValidatePatientPassword(string password)
        {
            return ValidatePassword(password, 12);
        }

        public static (bool IsValid, string Message) ValidateDoctorPassword(string password)
        {
            return ValidatePassword(password, 10);
        }

        private static (bool IsValid, string Message) ValidatePassword(string password, int minLength)
        {
            if (string.IsNullOrWhiteSpace(password))
                return (false, "Пароль не может быть пустым");

            if (password.Length < minLength)
                return (false, $"Пароль должен содержать минимум {minLength} символов");

            if (!Regex.IsMatch(password, @"[0-9]"))
                return (false, "Пароль должен содержать хотя бы одну цифру");

            if (!Regex.IsMatch(password, @"[!@#$%^&*()\[\],.?"":{}|<>~`'\\/_+=\-;]"))
                return (false, "Пароль должен содержать хотя бы один специальный символ");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return (false, "Пароль должен содержать хотя бы одну заглавную букву");

            return (true, "Пароль валиден");
        }
    }
}
