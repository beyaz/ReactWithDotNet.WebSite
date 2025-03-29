using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace ReactWithDotNet.VisualDesigner.Views;

static class ApplicationDatabase
{
    static readonly string CacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ReactVisualDesigner") +
                                            Path.DirectorySeparatorChar;

    static string ConnectionString => $"Data Source={CacheDirectory}app.db";

    static IReadOnlyList<ProjectEntity> Projects { get; set; }

    public static void DbOperation(Action<IDbConnection> operation)
    {
        using IDbConnection connection = new SqliteConnection(ConnectionString);

        operation(connection);
    }

    public static async Task DbOperation(Func<IDbConnection, Task> operation)
    {
        using IDbConnection connection = new SqliteConnection(ConnectionString);

        await operation(connection);
    }

    public static async Task<T> DbOperation<T>(Func<IDbConnection, Task<T>> operation)
    {
        using IDbConnection connection = new SqliteConnection(ConnectionString);

        return await operation(connection);
    }

    public static IReadOnlyList<ProjectEntity> GetAllProjects()
    {
        if (Projects is null)
        {
            const string query = "SELECT * FROM Project";

            DbOperation(connection => { Projects = connection.QueryAsync<ProjectEntity>(query).GetAwaiter().GetResult().ToList(); });
        }

        return Projects;
    }
}