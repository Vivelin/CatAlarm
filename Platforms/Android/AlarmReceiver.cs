using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;

using AndroidX.Core.App;

namespace MauiCatAlarm.Platforms.Android;

[BroadcastReceiver(Exported = true, Enabled = true)]
public class AlarmReceiver : BroadcastReceiver
{
    public const int NotificationId = 69;

    public override void OnReceive(Context? context, Intent? intent)
    {
        Log.Info("AlarmReceiver", "Alarm received");
        if (context == null)
            throw new InvalidOperationException("Context is null");

        var alarmIntent = new Intent(context, typeof(AlarmActivity));
        alarmIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
        var pendingIntent = PendingIntent.GetActivity(context, 0, alarmIntent, PendingIntentFlags.Immutable);

        var notificationManager = context.GetSystemService(Context.NotificationService).JavaCast<NotificationManager>();
        var notification = new NotificationCompat.Builder(context, MainApplication.ChannelId)
            .SetSmallIcon(Resource.Mipmap.appicon)
            .SetContentTitle("CAT ALARM!")
            .SetContentText("Mew mew mew mew mew mew!")
            .SetPriority(NotificationCompat.PriorityHigh)
            .SetCategory(NotificationCompat.CategoryAlarm)
            .SetContentIntent(pendingIntent)
            .SetFullScreenIntent(pendingIntent, true)
            .SetOngoing(true)
            .Build();

        notificationManager!.Notify(NotificationId, notification);
    }
}
