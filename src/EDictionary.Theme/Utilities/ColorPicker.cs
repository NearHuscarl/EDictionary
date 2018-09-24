using System;
using System.Windows;
using System.Windows.Media;

namespace EDictionary.Theme.Utilities
{
	/// <summary>
	/// Read color from Resource Dictionary based on resource key
	/// </summary>
	public static class ColorPicker
	{
		private static Uri colorUri = new Uri("pack://application:,,,/EDictionary.Theme;component/Styles/Colors.xaml", UriKind.RelativeOrAbsolute);
		private static readonly ResourceDictionary colorDict = new ResourceDictionary()
		{
			Source = colorUri,
		};

		public static System.Drawing.Color GetColor(string key)
		{
			SolidColorBrush brush = (SolidColorBrush)colorDict[key];

			return System.Drawing.Color.FromArgb(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B);
		}

		public static Color GetMediaColor(string key)
		{
			SolidColorBrush brush = (SolidColorBrush)colorDict[key];

			return Color.FromArgb(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B);
		}
	}
}
