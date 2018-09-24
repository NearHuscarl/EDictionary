﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EDictionary.Core.Extensions
{
	public static class EnumerableExtension
	{
		/// <summary>
		/// Take n last item in collection
		/// </summary>
		public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
		{
			return source.Skip(Math.Max(0, source.Count() - N));
		}
	}
}
