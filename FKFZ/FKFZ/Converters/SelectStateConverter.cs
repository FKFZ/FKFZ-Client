using System;
using System.Windows.Data;

namespace FKFZ.Converters
{
    public class SelectStateConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //double progress = (double)values[0];

            //ProgressBar progressBar = values[1] as ProgressBar;

            //string type = (string)parameter;
            //return 359.999 * (progress / (progressBar.Maximum - progressBar.Minimum));
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
