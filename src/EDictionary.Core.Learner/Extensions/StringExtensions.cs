using EDictionary.Core.Learner.Utilities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EDictionary.Core.Learner.Extensions
{
	public static class StringExtensions
	{
		private static Dictionary<string, Keys> keysDict = new Dictionary<string, Keys>()
		{
			{ "LControl", Keys.LControlKey },
			{ "RControl", Keys.RControlKey },
			{ "LAlt", Keys.LMenu },
			{ "RAlt", Keys.RMenu },
			{ "LWin", Keys.LWin },
			{ "RWin", Keys.RWin },
			{ "LShift", Keys.LShiftKey },
			{ "RShift", Keys.RShiftKey },
		};

		public static Keys ToKey(this string keyStr)
		{
			if (keysDict.ContainsKey(keyStr))
				return keysDict[keyStr];

			Keys key;

			var result = Enum.TryParse(keyStr, ignoreCase: true, result: out key);

			if (result)
				return key;

			return Keys.None;
		}

		public static string RemoveSpecialCharacters(this string str)
		{
			return Regex.Replace(str.TrimEnd(Environment.NewLine.ToCharArray()).Trim(), @"\t|\n|\r", " "); //Remove non-AsCII characters
		}
	}
}
