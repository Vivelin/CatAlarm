using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;

using AndroidX.Core.App;

namespace MauiCatAlarm.Platforms.Android;

[BroadcastReceiver(Exported = true, Enabled = true)]
public class AlarmReceiver : BroadcastReceiver
{
    public override void OnReceive(Context? context, Intent? intent)
    {
        Log.Info("AlarmReceiver", "Alarm received");
        if (context == null)
            throw new InvalidOperationException("Context is null");

        var mediaIntent = new Intent(context, typeof(AlarmMediaService));
        context.StartForegroundService(mediaIntent);
    }
}
