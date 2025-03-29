using System.Threading;
using Microsoft.Extensions.Hosting;

namespace ReactWithDotNet.VisualDesigner.Views;

public class ApplicationStateSaveService : BackgroundService
{
    static int AppStateVersion=2;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (ApplicationView.AppState is not null && AppStateVersion < ApplicationView.AppStateVersion)
            {
                await ApplicationStateCache.Save(ApplicationView.AppState);
                
                AppStateVersion = ApplicationView.AppStateVersion;
            }

            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
        }
    }
}