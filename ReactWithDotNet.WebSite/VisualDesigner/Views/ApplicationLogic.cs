using System.Data;
using System.IO;
using Dapper.Contrib.Extensions;

namespace ReactWithDotNet.VisualDesigner.Views;

static class ApplicationLogic
{
    public static ProjectConfigModel Project => DeserializeFromYaml<ProjectConfigModel>(File.ReadAllText(@"C:\github\ReactWithDotNet.WebSite\ReactWithDotNet.WebSite\VisualDesigner\Project.yaml"));

    public static Task<(bool fail, string failMessage)> CommitComponent(ApplicationState state)
    {
        return DbOperation(async db =>
        {
            var userVersion = (await db.GetComponentUserVersion(state.ProjectId, state.ComponentName, state.UserName)).Value;
            if (userVersion is null)
            {
                return (fail: true, failMessage: $"User ({state.UserName}) has no change to commit.");
            }

            var mainVersion = (await db.GetComponentMainVersion(state.ProjectId, state.ComponentName)).Value;

            // Check if the user version is the same as the main version
            if (mainVersion.PropsAsJson == userVersion.PropsAsJson &&
                mainVersion.StateAsJson == userVersion.StateAsJson &&
                mainVersion.RootElementAsJson == SerializeToJson(state.ComponentRootElement))
            {
                return (fail: true, failMessage: $"User ({state.UserName}) has no change to commit.");
            }

            userVersion = userVersion with
            {
                RootElementAsJson = SerializeToJson(state.ComponentRootElement)
            };

            mainVersion = mainVersion with
            {
                PropsAsJson = userVersion.PropsAsJson,
                StateAsJson = userVersion.StateAsJson,
                RootElementAsJson = userVersion.RootElementAsJson
            };

            await db.UpdateAsync(mainVersion);

            await db.DeleteAsync(userVersion);

            return default;
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

    public static IReadOnlyList<ComponentEntity> GetAllComponentsInProject(ApplicationState state)
    {
        var query = $"SELECT * FROM Component WHERE ProjectId = @{nameof(state.ProjectId)}";

        var dbRecords = DbOperation(async connection => (await connection.QueryAsync<ComponentEntity>(query, new { state.ProjectId })).ToList()).GetAwaiter().GetResult();

        return dbRecords;
    }

    public static async Task<Result<ComponentEntity>> GetComponentMainVersion(this IDbConnection db, ApplicationState state)
    {
        return await db.GetComponentMainVersion(state.ProjectId, state.ComponentName);
    }

    public static async Task<Result<ComponentEntity>> GetComponentMainVersion(this IDbConnection db, int projectId, string componentName)
    {
        if (projectId <= 0)
        {
            return new ArgumentException($"ProjectId: {projectId} is not valid");
        }

        if (componentName.HasNoValue())
        {
            return new ArgumentException($"ComponentName ({componentName}) is not valid");
        }

        const string sql = $"""
                            SELECT * 
                              FROM Component
                             WHERE {nameof(ComponentEntity.ProjectId)} = @{nameof(projectId)}
                               AND {nameof(ComponentEntity.Name)}      = @{nameof(componentName)}
                               AND {nameof(ComponentEntity.UserName)}  IS NULL OR {nameof(ComponentEntity.UserName)} = ''
                            """;

        return await db.QueryFirstAsync<ComponentEntity>(sql, new { projectId, componentName });
    }

    public static async Task<Result<ComponentEntity>> GetComponentUserVersion(this IDbConnection db, int projectId, string componentName, string userName)
    {
        if (projectId <= 0)
        {
            return new ArgumentException($"ProjectId: {projectId} is not valid");
        }

        if (componentName.HasNoValue())
        {
            return new ArgumentException($"ComponentName ({componentName}) is not valid");
        }

        if (userName.HasNoValue())
        {
            return new ArgumentException($"UserName ({userName}) is not valid");
        }

        const string sql = $"""
                            SELECT * 
                              FROM Component
                             WHERE {nameof(ComponentEntity.ProjectId)} = @{nameof(projectId)}
                               AND {nameof(ComponentEntity.Name)}      = @{nameof(componentName)}
                               AND {nameof(ComponentEntity.UserName)}  = @{nameof(userName)}
                            """;

        return await db.QueryFirstOrDefaultAsync<ComponentEntity>(sql, new { projectId, componentName, userName });
    }

    public static async Task<Result<ComponentEntity>> GetComponentUserVersion(this IDbConnection db, ApplicationState state)
    {
        return await db.GetComponentUserVersion(state.ProjectId, state.ComponentName, state.UserName);
    }

    public static async Task<Result<ComponentEntity>> GetComponentUserVersionNotNull(this IDbConnection db, int projectId, string componentName, string userName)
    {
        var userVersionResult = await db.GetComponentUserVersion(projectId, componentName, userName);
        if (userVersionResult.HasError)
        {
            return userVersionResult;
        }

        var userVersion = userVersionResult.Value;
        if (userVersion is not null)
        {
            return userVersion;
        }

        var mainVersionResult = await db.GetComponentMainVersion(projectId, componentName);
        if (mainVersionResult.HasError)
        {
            return mainVersionResult;
        }

        var mainVersion = mainVersionResult.Value;

        userVersion = mainVersion with
        {
            Id = 0,
            UserName = userName
        };

        await db.InsertAsync(userVersion);

        return userVersion;
    }

    public static Task<Result<ComponentEntity>> GetComponenUserOrMainVersion(int projectId, string componentName, string userName)
    {
        return DbOperation(async db => await db.GetComponentUserVersion(projectId, componentName, userName) ?? await db.GetComponentMainVersion(projectId, componentName));
    }
    
    public static Task<Result<ComponentEntity>> GetComponenUserOrMainVersion(ApplicationState state)
    {
        return DbOperation(async db => await db.GetComponentUserVersion(state.ProjectId, state.ComponentName, state.UserName) ?? await db.GetComponentMainVersion(state.ProjectId, state.ComponentName));
    }

    public static IReadOnlyList<string> GetProjectNames(ApplicationState state)
    {
        return GetAllProjects().Select(x => x.Name).ToList();
    }


    public static IReadOnlyList<string> GetStyleAttributeNameSuggestions(ApplicationState state)
    {
        var items = new List<string>();

        items.AddRange(Project.Styles.Keys);

        foreach (var colorName in Project.Colors.Select(x => x.Key))
        {
            items.Add("color: " + colorName);
        }

        // w
        {
            items.Add("w-full");
            items.Add("w-fit");
            items.Add("w-screen");
            items.Add("w-screen");
            for (var i = 1; i <= 100; i++)
            {
                if (i % 5 == 0)
                {
                    items.Add($"w-{i}vw");
                }
            }
        }

        // paddings
        {
            string[] names = ["padding", "padding-left", "padding-right", "padding-top", "padding-bottom"];

            foreach (var name in names)
            {
                for (var i = 1; i <= 1000; i++)
                {
                    if (i % 4 == 0)
                    {
                        items.Add($"{name}: {i}");
                    }
                }
            }
        }

        foreach (var propertyInfo in StyleProperties)
        {
            var attributeName = propertyInfo.Name;

            if (attributeName == "gap")
            {
                for (var i = 1; i < 100; i++)
                {
                    items.Add($"{attributeName}: {i * 4}");
                }
            }

            foreach (var suggestion in propertyInfo.Suggestions)
            {
                items.Add($"{attributeName}: {suggestion}");
            }
        }

        return items;
    }

    public static IReadOnlyList<string> GetStyleGroupConditionSuggestions(ApplicationState state)
    {
        return ["MD", "XXL", "state.user.isActive", "MD: state.user.isActive", "XXL: state.user.isActive"];
    }

    public static IReadOnlyList<string> GetSuggestionsForComponentSelection(ApplicationState state)
    {
        return GetAllComponentsInProject(state).Where(c => c.Name != state.ComponentName).Select(x => x.Name).ToList();
    }

    public static IReadOnlyList<string> GetTagSuggestions(ApplicationState state)
    {
        var suggestions = new List<string>(TagNameList);

        var allComponentsInProject = GetAllComponentsInProject(state);

        suggestions.AddRange(allComponentsInProject.Where(c => c.Name != state.ComponentName).Select(x => x.Name));

        return suggestions;
    }

    public static StyleModifier TryProcessStyleAttributeByProjectConfig(string styleAttribute)
    {
        {
            var (success, name, value) = TryParsePropertyValue(styleAttribute);
            if (success)
            {
                if (name == "color")
                {
                    if (Project.Colors.TryGetValue(value, out var realColor))
                    {
                        return Color(realColor);
                    }
                }
            }
        }

        {
            if (!Project.Styles.TryGetValue(styleAttribute, out var value))
            {
                return null;
            }

            return Style.ParseCss(value);
        }
    }

    public static Task<Result> TrySaveComponentForUser(ApplicationState state)
    {
        if (state.ProjectId <= 0 || state.ComponentName.HasNoValue())
        {
            return Task.FromResult(Success);
        }

        return DbOperation(async db =>
        {
            var userVersionResult = await db.GetComponentUserVersion(state.ProjectId, state.ComponentName, state.UserName);
            if (userVersionResult.HasError)
            {
                return userVersionResult.Error;
            }

            var userVersion = userVersionResult.Value;

            if (userVersion is null)
            {
                var mainVersionResult = await db.GetComponentMainVersion(state.ProjectId, state.ComponentName);
                if (mainVersionResult.HasError)
                {
                    return mainVersionResult.Error;
                }

                var mainVersion = mainVersionResult.Value;

                if (SerializeToJson(state.ComponentRootElement) == mainVersion.RootElementAsJson)
                {
                    return Success;
                }

                await db.InsertAsync(mainVersion with
                {
                    Id = 0,
                    RootElementAsJson = SerializeToJson(state.ComponentRootElement),
                    UserName = state.UserName
                });

                return Success;
            }

            await db.UpdateAsync(userVersion with
            {
                RootElementAsJson = SerializeToJson(state.ComponentRootElement)
            });

            return Success;
        });
    }

    public static Task UpdateLastUsageInfo(ApplicationState state)
    {
        if (state.ProjectId <= 0)
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

    public static Task<Result> UpdateUserVersion(ApplicationState state, Func<ComponentEntity, ComponentEntity> modify)
    {
        return DbOperation(async db =>
        {
            var userVersionResult = await db.GetComponentUserVersionNotNull(state.ProjectId, state.ComponentName, state.UserName);
            if (userVersionResult.HasError)
            {
                return userVersionResult.Error;
            }

            var userVersion = userVersionResult.Value;

            userVersion = modify(userVersion);

            await db.UpdateAsync(userVersion);

            return Success;
        });
    }
}