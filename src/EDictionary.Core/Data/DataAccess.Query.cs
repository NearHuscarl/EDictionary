namespace EDictionary.Core.Data
{
	public partial class DataAccess
	{
		private static readonly string tableName = "[Dictionary]";

		private readonly string createTableQuery = $@"
			CREATE TABLE IF NOT EXISTS {tableName}
			(
				[ID] NVARCHAR,
				[Name] NVARCHAR,
				[Definition] NVARCHAR
			);";

		private readonly string insertQuery = $@"
			INSERT INTO {tableName}
			(
				[ID],
				[Name],
				[Definition]
			)
			VALUES
			(
				@id,
				@name,
				@definition
			)";

		private readonly string selectDefinitionFromQuery = $@"
			SELECT [Definition] FROM {tableName}
			WHERE [ID] = @id";

		private readonly string selectIDQuery = $@"
			SELECT [ID]
			FROM {tableName}
			ORDER BY [ID] ASC";

		private readonly string selectNameQuery = $@"
			SELECT [Name]
			FROM {tableName}
			ORDER BY [ID] ASC";

		private readonly string selectIDAndNameQuery = $@"
			SELECT [ID], [Name]
			FROM {tableName}";
	}
}
