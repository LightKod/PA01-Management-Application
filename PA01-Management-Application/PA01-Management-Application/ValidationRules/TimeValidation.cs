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
    public class TimeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";
            string input = (string)value;

            if (Regex.IsMatch(input, pattern) || input == string.Empty)
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Invalid time");
        }
    }
}
