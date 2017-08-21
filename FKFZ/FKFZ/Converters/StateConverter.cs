using FKFZ.Controls;
using System;
using System.Windows.Data;

namespace FKFZ.Converters
{
    public class StateConverter : IValueConverter
    {

        public object Convert(object values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int type = (int)values;
            if (type == 1)
            {
                return ResultState.RIGHT;
            }
            else if (type == 2) {
                return ResultState.ERROR;
            }
            return ResultState.UNSELECT;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            ResultState state = (ResultState)value;
            switch (state)
            {
                case ResultState.ERROR:
                    return 2;
                case ResultState.RIGHT:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
