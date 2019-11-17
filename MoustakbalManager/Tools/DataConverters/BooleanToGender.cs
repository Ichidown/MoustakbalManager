using System;
using System.Globalization;
using System.Windows.Data;

namespace MoustakbalManager.Tools.DataConverters
{
	public class BooleanToGender : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (Boolean)value ? 'F' : 'M';
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (Char)value == 'F' ? true : false;
		}
	}
}
