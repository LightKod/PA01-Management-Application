using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PA01_Management_Application.ValidationRules
{
    public class PositiveNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (string)value;
            int number = 0;

            try
            {
                if (input.Length > 0)
                    number = Int32.Parse((String)input);
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }

            if (input == string.Empty || number > 0)
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Invalid input");
        }
    }
}
