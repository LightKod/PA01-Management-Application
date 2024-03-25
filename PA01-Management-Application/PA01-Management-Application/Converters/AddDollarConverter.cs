using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PA01_Management_Application.Converters
{
    public class AddDollarConverter : IValueConverter
    {
        // Convert from source (string) to target (string)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float floatValue)
            {
                // Format the float value as currency with $ symbol and two decimal places
                return floatValue.ToString("C", culture);
            }else if (value is double doubleValue)
            {
                return doubleValue.ToString("C", culture);
            }

            // Return original value if not a string
            return value;
        }

        // Convert back from target (string) to source (string)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
