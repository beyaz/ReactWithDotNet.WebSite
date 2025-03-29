using System.IO;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ReactWithDotNet.VisualDesigner.Views;

class ApplicationDatabase
{
    static readonly string CacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ReactVisualDesigner") +
                                            Path.DirectorySeparatorChar;

    static string ConnectionString => $"Data Source={CacheDirectory}app.db";

    static IReadOnlyList<ProjectEntity> Projects { get; set; }

    public static IReadOnlyList<ProjectEntity> GetAllProjects()
    {
        if (Projects is null)
        {
            const string query = "SELECT * FROM Project";

            using var connection = new SqliteConnection(ConnectionString);

            Projects = connection.QueryAsync<ProjectEntity>(query).GetAwaiter().GetResult().ToList();
        }

        return Projects;
    }
}