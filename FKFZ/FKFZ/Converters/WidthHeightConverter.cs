using System;
using System.Windows.Data;

namespace FKFZ.Converters
{
    public class WidthHeightConverter : IValueConverter
    {

        public object Convert(object values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double width = (double)values;
            double rito = Double.Parse((String)parameter);
            return width/rito;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
