using EDictionary.Core.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EDictionary.Core.Converters
{
	public class OptionToRadioBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			VocabularySource option = (VocabularySource)value;
			
			return option.Equals(parameter);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return parameter;
		}
	}
}
