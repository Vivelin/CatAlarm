using Android.App;
using Android.Content;
using Android.Util;

using MauiCatAlarm.Services;

namespace MauiCatAlarm.Platforms.Android.Receivers;

[BroadcastReceiver(Enabled = true, Exported = true)]
[IntentFilter([Intent.ActionBootCompleted])]
public class BootReceiver : BroadcastReceiver
{
    public override void OnReceive(Context? context, Intent? intent)
    {
        Log.Info("BootReceiver", $"OnReceive {intent}");

        var alarmService = new AlarmService();
        alarmService.EnsureAlarmIsSetIfEnabled();
    }
}
