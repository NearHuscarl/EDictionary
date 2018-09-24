using System.Collections.Generic;
using System.Linq;

namespace EDictionary.Core.Utilities
{
	public static class Search
	{
		/// <summary>
		/// Prefix search using binary search algorithm to search <element> in <sequence>
		/// recursive=False: if element not found as a prefix of any word in sequence simply return -1
		/// recursive=True:  if element not found as a prefix of any word in sequence check if every
		/// prefix of that element match any prefix in sequence, if not again return -1
		/// </summary>
		public static int Prefix(string element, List<string> sequence, bool recursive=true)
		{
			int minPos = 0;
			int maxPos = sequence.Count - 1;
			int prefixPos = -1;

			while (true)
			{
				if (maxPos < minPos)
				{
					if (prefixPos != -1)
						return prefixPos;

					if (recursive)
						foreach (var i in Enumerable.Range(0, element.Length + 1))
						{
							int pos = Prefix(element.Substring(0, i), sequence, recursive=false);

							if (pos != -1)
								prefixPos = pos;
							else
								return prefixPos;

							//return -1;
						}

					return -1;
				}

				int curPos = (int)((minPos + maxPos) / 2);
				string currentWord = sequence[curPos].ToLower();

				if (currentWord.StartsWith(element))
					prefixPos = curPos;

				if (currentWord.CompareTo(element) < 0)
					minPos = curPos + 1;

				else if (currentWord.CompareTo(element) > 0)
					maxPos = curPos - 1;

				else // sequence[curPos] == element
					return curPos;
			}
		}
	}
}
