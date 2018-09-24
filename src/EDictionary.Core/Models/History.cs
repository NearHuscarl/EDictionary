using EDictionary.Core.Extensions;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace EDictionary.Core.Models
{
	[Serializable]
	public class History<T>
	{
		[XmlIgnore]
		public static readonly string Directory = Path.Combine(ApplicationPath.ApplicationData, "data");
		[XmlIgnore]
		public static readonly string FullPath = Path.Combine(Directory, "history.xml");

		[XmlIgnore]
		public static History<T> Default = new History<T>()
		{
			Collection = new List<T>(),
			MaxHistory = 1000,
			CurrentIndex = -1,
		};

		#region Properties

		public int MaxHistory { get; set; } = 1000; // In case xml History field is empty
		public int CurrentIndex { get; set; }

		public List<T> Collection { get; set; }

		public T Current
		{
			get
			{
				if (CurrentIndex == -1 || Collection.Count == 0)
					return default(T);

				return Collection[CurrentIndex];
			}
		}

		public bool IsFirst
		{
			get { return CurrentIndex == 0; }
		}

		public bool IsLast
		{
			get { return CurrentIndex == Collection.Count - 1; }
		}

		public int Count
		{
			get { return Collection.Count; }
		}

		#endregion

		/// <summary>
		/// add history item at the next index and truncate the rest
		/// </summary>
		public void Add(T item)
		{
			if (CurrentIndex + 1 <= Collection.Count - 1)
			{
				Collection.Insert(CurrentIndex + 1, item);
			}
			else
			{
				Collection.Add(item);
			}

			CurrentIndex++;
			Collection = Collection.Take(CurrentIndex + 1).ToList();
		}

		public T Previous()
		{
			if (CurrentIndex > 0)
				return Collection[--CurrentIndex];

			return Collection[0];
		}

		public T Next()
		{
			if (CurrentIndex < Collection.Count - 1)
				return Collection[++CurrentIndex];

			return Collection[Collection.Count - 1];
		}

		/// <summary>
		/// Remove old history based on MaxHistory
		/// </summary>
		public void Trim()
		{
			if (Collection.Count > MaxHistory)
			{
				Collection = (List<T>)Collection.TakeLast(MaxHistory);
			}
		}
	}
}
