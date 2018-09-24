using System;
using System.IO;

namespace EDictionary.Core.Data.Factory
{
	public static class DatabaseFactory
	{
		public static DataAccess CreateWordlistDatabase()
		{
			DatabaseInfo dbInfo = new DatabaseInfo();

			//dbInfo.SaveDir = ApplicationPath.CommonApplicationData;
			dbInfo.SaveDir = AppDomain.CurrentDomain.BaseDirectory;
			dbInfo.SavePath = Path.Combine(dbInfo.SaveDir, "words.sqlite");
			dbInfo.ConnectionString = $"Data Source={dbInfo.SavePath};Version=3;";

			return new DataAccess(dbInfo);
		}

		//public static HistoryAccess CreateHistoryDatabase()
		//{
		//	DatabaseInfo dbInfo = new DatabaseInfo();

		//	dbInfo.SaveDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
		//	dbInfo.SavePath = Path.Combine(dbInfo.SaveDir, "history.sqlite");
		//	dbInfo.ConnectionString = $"Data Source={dbInfo.SavePath};Version=3;";

		//	return new HistoryAccess(dbInfo);
		//}
	}
}