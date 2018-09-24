using System;
using System.Text.RegularExpressions;

namespace EDictionary.Core.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		/// return regex match of str with pattern
		/// </summary>
		/// <param name="str">string to apply regex search on</param>
		/// <param name="pattern">regex pattern</param>
		/// <returns></returns>
		public static string MatchRegex(this string str, string pattern)
		{
			Regex regexPattern = new Regex(pattern);
			var result = regexPattern.Match(str);

			return result.Value;
		}
	}
}
