using System.Collections.Generic;

namespace EDictionary.Core.Learner.Extensions
{
	public static class ListExtensions
	{
		public static T Pop<T>(this List<T> list)
		{
			return list.PopAt(list.Count - 1);
		}

		public static T PopAt<T>(this List<T> list, int index)
		{
			T r = list[index];
			list.RemoveAt(index);
			return r;
		}
	}
}
