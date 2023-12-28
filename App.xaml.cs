using MauiCatAlarm.Services;

using Microsoft.Extensions.Logging;

namespace MauiCatAlarm;

public partial class App : Application
{
    private IServiceScope _serviceScope;

    public App(IServiceProvider serviceProvider, ILogger<App> logger)
    {
        _serviceScope = serviceProvider.CreateScope();

        InitializeComponent();

        MainPage = ServiceProvider.GetRequiredService<MainPage>();    
    }

    public IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

    protected override void CleanUp()
    {
        _serviceScope.Dispose();
        base.CleanUp();
    }
}
