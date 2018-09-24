using System;
using System.Windows;
using System.Windows.Data;

namespace EDictionary.Core.Converters
{
	public class StringToResourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value != null)
				return Application.Current.FindResource((string)value);

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
