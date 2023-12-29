using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;

using MauiCatAlarm.Platforms.Android;

namespace MauiCatAlarm;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize
                           | ConfigChanges.Orientation
                           | ConfigChanges.UiMode
                           | ConfigChanges.ScreenLayout
                           | ConfigChanges.SmallestScreenSize
                           | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
//    private readonly ServiceConnection _serviceConnection;

//    public MainActivity()
//    {
//        _serviceConnection = new ServiceConnection(this);
//    }

//    private ActiveAlarmService? ActiveAlarm { get; set; }

//    protected override void OnStart()
//    {
//        base.OnStart();

//        var intent = new Intent(this, typeof(ActiveAlarmService));
//        BindService(intent, _serviceConnection, Bind.AutoCreate);
//    }

//    protected override void OnStop()
//    {
//        base.OnStop();

//        if (ActiveAlarm != null)
//        {
//            UnbindService(_serviceConnection);
//        }
//    }

//    private class ServiceConnection(MainActivity mainActivity) : Java.Lang.Object, IServiceConnection
//    {
//        private readonly MainActivity _mainActivity = mainActivity;

//        public void OnServiceConnected(ComponentName? name, IBinder? service)
//        {
//            if (service is ActiveAlarmService.LocalBinder binder)
//            {
//                _mainActivity.ActiveAlarm = binder.Service;
//                Log.Info("MainActivity", $"Connected to {name}");
//            }
//        }

//        public void OnServiceDisconnected(ComponentName? name)
//        {
//            _mainActivity.ActiveAlarm = null;
//            Log.Info("MainActivity", $"Disconnected from {name}");
//        }
//    }
}
