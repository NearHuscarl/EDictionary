using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Linq;
using EDictionary.Core.Utilities;
using EDictionary.Core.Models;
using EDictionary.Core.Extensions;

namespace EDictionary.Core.Data
{
	public partial class DataAccess : SqliteAccess
	{
		public DataAccess(DatabaseInfo dbInfo) : base(dbInfo)
		{

		}

		public override Result CreateTable()
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(createTableQuery, dbConnection))
				{
					OpenConnection();
					command.ExecuteNonQuery();

					return new Result(message:"", innerMessage:"", status:Status.Success);
				}
			}
			catch (Exception exception)
			{
				return new Result(message:exception.Message, status:Status.Error, exception:exception);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result Insert(string wordJsonStr)
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(insertQuery, dbConnection))
				{
					OpenConnection();

					Word word = JsonHelper.Deserialize(wordJsonStr);
					string wordNumber = word.ID.MatchRegex("[0-9]");
					string wordName = word.Name;

					if (wordNumber != null)
						wordName += " " + wordNumber;

					command.Parameters.AddWithValue("@id", word.ID);
					command.Parameters.AddWithValue("@name", wordName); // TODO: Test
					command.Parameters.AddWithValue("@definition", wordJsonStr);

					command.ExecuteNonQuery();

					return new Result(message:"", innerMessage:"", status:Status.Success);
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at Insert in DataAccess:\n{exception.Message}");
				return new Result(message:exception.Message, status:Status.Error, exception:exception);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result<string> SelectDefinitionFrom(string wordID)
		{
			Watcher watch = new Watcher();

			try
			{
				using (SQLiteCommand command = new SQLiteCommand(selectDefinitionFromQuery, dbConnection))
				{
					OpenConnection();

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", Value = wordID});
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						reader.Read();
						string definitionStr = Convert.ToString(reader["Definition"]);

						return new Result<string>(data:definitionStr, message:"", status:Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at SelectDefinitionFrom in DataAccess:\n{exception.Message}");
				
				return new Result<string>(
						message:exception.Message,
						innerMessage:"",
						status:Status.Error,
						exception:exception
						);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result<List<string>> SelectID()
		{
			try
			{
				List<string> results = new List<string>();

				using (SQLiteCommand command = new SQLiteCommand(selectIDQuery, dbConnection))
				{
					OpenConnection();

					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(Convert.ToString(reader["ID"]));
						}
						return new Result<List<string>>(data:results, message:"", status:Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at SelectID in DataAccess:\n{exception.Message}");

				return new Result<List<string>>(
						message:exception.Message,
						innerMessage:"",
						status:Status.Error,
						exception:exception
						);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result<List<string>> SelectName()
		{
			try
			{
				List<string> results = new List<string>();
				using (SQLiteCommand command = new SQLiteCommand(selectNameQuery, dbConnection))
				{
					OpenConnection();
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(Convert.ToString(reader["Name"]));
						}
						return new Result<List<string>>(data: results, message: "", status: Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at SelectName in DataAccess:\n{exception.Message}");

				return new Result<List<string>>(
						message: exception.Message,
						innerMessage: "",
						status: Status.Error,
						exception: exception
						);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result<Dictionary<string, string>> SelectIDAndName()
		{
			try
			{
				Dictionary<string, string> results = new Dictionary<string, string>();
				using (SQLiteCommand command = new SQLiteCommand(selectIDAndNameQuery, dbConnection))
				{
					OpenConnection();
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(
								Convert.ToString(reader["ID"]),
								Convert.ToString(reader["Name"]));
						}
						return new Result<Dictionary<string, string>>(data: results, message: "", status: Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at SelectIDAndName in DataAccess:\n{exception.Message}");

				return new Result<Dictionary<string, string>>(
						message: exception.Message,
						innerMessage: "",
						status: Status.Error,
						exception: exception
						);
			}
			finally
			{
				CloseConnection();
			}
		}
	}
}
