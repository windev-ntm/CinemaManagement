using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CinemaManagement
{
    public class UsernameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Username = value as string;

            Regex regexUsername = new Regex(@"^[a-zA-Z0-9_]{3,20}$");

            if (!regexUsername.IsMatch(Username))
            {
                return new ValidationResult(false, "Username must consist of 3 to 20 characters and can only contain letters, numbers, and underscores.");
            }
            return ValidationResult.ValidResult;
        }
    }
    class PasswordRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Password = value as string;

            Regex regexPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()-_+=])[A-Za-z\d!@#$%^&*()-_+=]{8,}$");

            if (!regexPassword.IsMatch(Password))
            {
                return new ValidationResult(false, "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime selectedDate;

            if (value == null || !DateTime.TryParse(value.ToString(), out selectedDate))
            {
                return new ValidationResult(false, "Please select a valid date.");
            }

            // Add more complex validation if needed

            return ValidationResult.ValidResult;
        }
    }

    public class ComboBoxValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(false, "Please select a gender.");
            }

            return ValidationResult.ValidResult;
        }
    }

    class EmailRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Email = value as string;

            if (string.IsNullOrEmpty(Email))
            {
                return new ValidationResult(false, "Email is required");
            }

            Regex regexEmail = new Regex(@"^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,6})*$");
            if (!regexEmail.IsMatch(Email))
            {
                return new ValidationResult(false, "Email is invalid");
            }

            /*if (string.IsNullOrEmpty(Email))
            {
                return new ValidationResult(false, "Email is required");
            }
            if (string.IsNullOrEmpty(Address))
            {
                return new ValidationResult(false, "Address is required");
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                return new ValidationResult(false, "PhoneNumber is required");
            }
            if (string.IsNullOrEmpty(Image))
            {
                return new ValidationResult(false, "Image is required");
            }*/





            return ValidationResult.ValidResult;
        }
    }

    class AddressRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Address = value as string;

            if (string.IsNullOrEmpty(Address))
            {
                return new ValidationResult(false, "Address is required");
            }
            return ValidationResult.ValidResult;
        }
    }
    class PhoneNumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string PhoneNumber = value as string;

            if (string.IsNullOrEmpty(PhoneNumber))
            {
                return new ValidationResult(false, "PhoneNumber is required");
            }
            Regex regexPhone = new Regex(@"^\d{10}$");

            if (!regexPhone.IsMatch(PhoneNumber))
            {
                return new ValidationResult(false, "PhoneNumber is invalid");
            }
            return ValidationResult.ValidResult;
        }
    }
    class ImageRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Image = value as string;

            Regex regexImage = new Regex(@"^[0-9a-zA-Z]+.png$");

            if (!regexImage.IsMatch(Image))
            {
                return new ValidationResult(false, "Image is invalid");
            }
            return ValidationResult.ValidResult;
        }
    }
}
