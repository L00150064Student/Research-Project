
using System.Globalization;

namespace v4MobileApp.Converter;

public class SliderValueToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int intValue)
        {
            if (intValue <= 6)
            {
                return Colors.Green;
            }
            else if (intValue <= 8)
            {
                return Colors.Orange;
            }
            else
            {
                return Colors.Red;
            }
        }

        return Colors.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
