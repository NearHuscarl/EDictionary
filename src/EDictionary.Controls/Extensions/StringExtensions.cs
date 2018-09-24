using System;

namespace EDictionary.Controls.Extensions
{
	internal static class StringExtensions
	{
		public static string[] SplitWord(this string str)
		{
			return str.Split(new char[] { ' ', '\n', ',', '/' }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
