using Android.Content;
using Android.Util;

namespace MauiCatAlarm.Platforms.Android;

[BroadcastReceiver(Exported = true, Enabled = true)]
public class AlarmReceiver : BroadcastReceiver
{
    public override void OnReceive(Context? context, Intent? intent)
    {
        Log.Info("AlarmReceiver", "Alarm received");
        if (context == null)
            throw new InvalidOperationException("Context is null");

        var activityIntent = new Intent(context, typeof(AlarmActivity));
        activityIntent.SetFlags(ActivityFlags.NewTask);

        Log.Info("AlarmReceiver", "Starting activity");
        context.StartActivity(activityIntent);
        Log.Info("AlarmReceiver", "Activity started");
    }
}
