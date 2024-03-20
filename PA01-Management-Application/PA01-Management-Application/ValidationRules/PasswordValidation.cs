using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PA01_Management_Application.ValidationRules
{
    public class PasswordValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (string)value;

            // Kiểm tra mật khẩu có ít nhất 8 ký tự, chứa chữ cái và số
            if (Regex.IsMatch(input, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Password must be at least 8 characters long and contain at least one letter and one digit");
            }
        }
    }
}
