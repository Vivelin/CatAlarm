using Android.Content;
using Android.Util;

namespace MauiCatAlarm.Platforms.Android;

[BroadcastReceiver(Exported = true, Enabled = true)]
public class AlarmReceiver : BroadcastReceiver
{
    public override void OnReceive(Context? context, Intent? intent)
    {
        Log.Info("AlarmReceiver", "Alarm received");
    }
}
