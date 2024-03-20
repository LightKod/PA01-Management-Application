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
    public class EmailValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (string)value;

            // Kiểm tra định dạng email
            if (Regex.IsMatch(input, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Invalid email format");
            }
        }
    }
}
