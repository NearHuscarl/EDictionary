using EDictionary.Core.Utilities;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace EDictionary.Core.Data
{
	public abstract class SqliteAccess
	{
		protected readonly DatabaseInfo dbInfo;
		protected SQLiteConnection dbConnection;

		public abstract Result CreateTable();

		public SqliteAccess(DatabaseInfo dbInfo)
		{
			this.dbInfo = dbInfo;

			InitializeDatabase();
		}

		protected void InitializeDatabase()
		{
			if (!File.Exists(dbInfo.SavePath))
			{
				Directory.CreateDirectory(dbInfo.SaveDir); // create directory if not exists
				SQLiteConnection.CreateFile(dbInfo.SavePath);
				dbConnection = new SQLiteConnection(dbInfo.ConnectionString);
				CreateTable();
			}
			else
			{
				dbConnection = new SQLiteConnection(dbInfo.ConnectionString);
			}
		}

		protected void OpenConnection()
		{
			if (dbConnection.State != ConnectionState.Open)
				dbConnection.Open();
		}

		protected void CloseConnection()
		{
			if (dbConnection.State != ConnectionState.Closed)
				dbConnection.Close();
		}
	}
}
