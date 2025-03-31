using Dapper.Contrib.Extensions;

namespace ReactWithDotNet.VisualDesigner.Views;

static class ApplicationLogic
{
    public static Task DbOperationForCurrentComponent(ApplicationState state, Func<ComponentEntity, Task> operation)
    {
        return DbOperation(async connection =>
        {
            var dbRecord = await connection.GetAsync<ComponentEntity>(state.ComponentId);

            await operation(dbRecord);
        });
    }

    public static Task DbOperationForCurrentComponent(ApplicationState state, Action<ComponentEntity> operation)
    {
        return DbOperation(async connection =>
        {
            var dbRecord = await connection.GetAsync<ComponentEntity>(state.ComponentId);

            operation(dbRecord);
        });
    }

    public static Task DbSave(ApplicationState state, Func<ComponentEntity, ComponentEntity> modify)
    {
        return DbOperation(async connection =>
        {
            var dbRecord = await connection.GetAsync<ComponentEntity>(state.ComponentId);

            dbRecord = modify(dbRecord);

            await connection.UpdateAsync(dbRecord);
        });
    }

    public static IReadOnlyList<ComponentEntity> GetAllComponentsInProject(ApplicationState state)
    {
        var query = $"SELECT * FROM Component WHERE ProjectId = @{nameof(state.ProjectId)}";

        var dbRecords = DbOperation(async connection => (await connection.QueryAsync<ComponentEntity>(query, new{state.ProjectId})).ToList()).GetAwaiter().GetResult();

        return dbRecords;
    }

    public static IReadOnlyList<string> GetProjectNames(ApplicationState state)
    {
        return GetAllProjects().Select(x => x.Name).ToList();
    }

    public static ComponentEntity GetSelectedComponent(ApplicationState state)
    {
        var query = $"SELECT * FROM Component WHERE Id = @{nameof(state.ComponentId)}";

        var dbRecords = DbOperation(async connection => (await connection.QueryAsync<ComponentEntity>(query, new { state.ComponentId })).ToList()).GetAwaiter().GetResult();

        
        
        return dbRecords.FirstOrDefault(x=>x.UserName == state.UserName) ?? dbRecords.FirstOrDefault();
    }
    
    public static Task UpdateLastUsageInfo(ApplicationState state)
    {
        if (state.ComponentId <= 0)
        {
            return Task.CompletedTask;
        }
        
        const string query = $"SELECT * FROM LastUsageInfo WHERE UserName = @{nameof(state.UserName)}";

        return DbOperation(async db =>
        {
            var dbRecords = await db.QueryAsync<LastUsageInfoEntity>(query, new { state.UserName });

            var dbRecord = dbRecords.FirstOrDefault(x => x.ProjectId == state.ProjectId);
            if (dbRecord is not null)
            {
                await db.UpdateAsync(dbRecord with
                {
                    AccessTime = DateTime.Now,
                    StateAsJson = SerializeToJson(state)
                });
            }
            else
            {
                await db.InsertAsync(new LastUsageInfoEntity
                {
                    UserName    = state.UserName, 
                    ProjectId   = state.ProjectId,
                    AccessTime  = DateTime.Now,
                    StateAsJson = SerializeToJson(state)
                });
            }
        });
    }

    public static IReadOnlyList<string> GetStyleGroupConditionSuggestions(ApplicationState state)
    {
        return ["MD", "XXL", "state.user.isActive", "MD: state.user.isActive", "XXL: state.user.isActive"];
    }

    public static IReadOnlyList<string> GetSuggestionsForComponentSelection(ApplicationState state)
    {
        return GetAllComponentsInProject(state).Where(c => c.Id != state.ComponentId).Select(x => x.Name).ToList();
    }

    public static IReadOnlyList<string> GetTagSuggestions(ApplicationState state)
    {
        var suggestions = new List<string>(TagNameList);

        var allComponentsInProject = GetAllComponentsInProject(state);

        suggestions.AddRange(allComponentsInProject.Where(c => c.Id != state.ComponentId).Select(x => x.Name));

        return suggestions;
    }
}