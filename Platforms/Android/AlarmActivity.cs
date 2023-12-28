using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;

using Microsoft.Maui.Platform;

namespace MauiCatAlarm.Platforms.Android;

[Activity(Theme = "@style/Maui.SplashTheme",
    MainLauncher = false,
    NoHistory = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
    ShowForAllUsers = true,
    TurnScreenOn = true,
    Enabled = true,
    Exported = true)]
public class AlarmActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        Log.Info("AlarmActivity", "OnCreate");
        base.OnCreate(savedInstanceState);

        Window.AddFlags(WindowManagerFlags.ShowWhenLocked);
        Window.AddFlags(WindowManagerFlags.DismissKeyguard);
        Window.AddFlags(WindowManagerFlags.KeepScreenOn);
        Window.AddFlags(WindowManagerFlags.TurnScreenOn);

        if (App.Current is not App app)
            throw new InvalidOperationException("Could not find App instance.");

        var alarmPage = app.ServiceProvider.GetRequiredService<AlarmPage>();
        app.OpenWindow(new Microsoft.Maui.Controls.Window(alarmPage));
        Log.Info("AlarmActivity", "Opened new window with AlarmPage");
    }
}
