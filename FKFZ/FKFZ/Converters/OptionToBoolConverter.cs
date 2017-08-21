using System;
using System.Windows;
using System.Windows.Data;

namespace FKFZ.Converters
{
    internal class OptionToBoolConverter : DependencyObject,IValueConverter, IMultiValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            String answerid = (String)value;
            return true;//s == (Sex)int.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isChecked = (bool)value;
            if (!isChecked)
            {
                //判断value的值为false的时候，会直接返回null,是为了RadioButton的状态变为未选中的时候，阻止数据传回Employee的实例
                return null;
            }
            return true;// (Sex)int.Parse(parameter.ToString());
        }

        #endregion

        public String UserName
        {
            get { return (String)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(String), typeof(OptionToBoolConverter), new UIPropertyMetadata(null));

        #region IMultiValueConverter Members
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(values[0], targetType, values[1], culture);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object[] { ConvertBack(value, targetTypes[0], parameter, culture) };
        }
        #endregion
    }
}
