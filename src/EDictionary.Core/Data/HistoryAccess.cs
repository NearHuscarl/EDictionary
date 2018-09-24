using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.IO;
using System.Xml.Serialization;

namespace EDictionary.Core.Data
{
	public class HistoryAccess
	{
		public Result<History<T>> LoadHistory<T>()
		{
			try
			{
				History<T> history = new History<T>();

				using (FileStream stream = new FileStream(History<T>.FullPath, FileMode.Open))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(History<T>));
					history = serializer.Deserialize(stream) as History<T>;
				}

				return new Result<History<T>>(history, "Loading history successful", "", Status.Success);
			}
			catch (Exception ex)
			{
				return new Result<History<T>>(null, "Error occured on loading history", "", Status.Error, ex);
			}
		}

		public Result SaveHistory<T>(History<T> history)
		{
			try
			{
				if (!Directory.Exists(History<T>.Directory))
					Directory.CreateDirectory(History<T>.Directory);

				using (FileStream stream = new FileStream(History<T>.FullPath, FileMode.Create))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(History<T>));
					serializer.Serialize(stream, history);
				}

				return new Result(Status.Success);
			}
			catch (Exception ex)
			{
				return new Result("Error occured on saving history", Status.Error, ex);
			}
		}
	}
}
