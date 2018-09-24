using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EDictionary.Core.Extensions
{
	public static class ListExtension
	{
		private static Random random = new Random();

		public static T NextItem<T>(this List<T> list, int currentIndex)
		{
			if (currentIndex < 0 || currentIndex >= list.Count - 1)
				return default(T);

			return list[currentIndex + 1];
		}

		public static T PickRandom<T>(this List<T> source)
		{
			return source.PickRandom(1).FirstOrDefault();
		}

		public static List<T> PickRandom<T>(this List<T> source, int count)
		{
			List<T> result = new List<T>();

			foreach(var i in Enumerable.Range(1, count))
			{
				result.Add(source[random.Next(source.Count)]);
			}

			return result;
		}
	}
}
