using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace EDictionary.Core.Converters
{
	public class ListToStringConverter : IValueConverter
   {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return string.Join(Environment.NewLine, ((List<string>)value).ToArray());
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string str = (string)value;

			if (str == "")
				return new List<string>();

			return str.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
		}
	}
}
