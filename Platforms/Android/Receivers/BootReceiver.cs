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
        var scheduledTime = alarmService.GetScheduledTime();
        if (scheduledTime == null)
        {
            Log.Info("BootReceiver", $"No alarm scheduled");
            return;
        }

        Log.Info("BootReceiver", $"Setting alarm for {scheduledTime}");
        alarmService.SetAlarm(scheduledTime.Value);
        Log.Info("BootReceiver", $"Alarm set for {scheduledTime}");
    }
}
