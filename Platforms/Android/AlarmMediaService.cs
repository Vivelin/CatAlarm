using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

using AndroidX.Core.App;

namespace MauiCatAlarm.Platforms.Android;

[Service(ForegroundServiceType = ForegroundService.TypeMediaPlayback)]
public class AlarmMediaService : Service
{
    public const int NotificationId = 69;

    public override IBinder? OnBind(Intent? intent) => null;

    [return: GeneratedEnum]
    public override StartCommandResult OnStartCommand(Intent? intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    {
        var context = ApplicationContext ?? throw new InvalidOperationException("Context not available.");

        // if (OperatingSystem.IsAndroidVersionAtLeast(33))
        // {
        //     var notificationPermission = context.CheckSelfPermission(global::Android.Manifest.Permission.PostNotifications);
        //     if (notificationPermission == Permission.Denied)
        //     {
        //         StopSelf();
        //         return StartCommandResult.NotSticky;
        //     }
        // }

        var alarmIntent = new Intent(context, typeof(AlarmActivity));
        alarmIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
        var pendingIntent = PendingIntent.GetActivity(context, 0, alarmIntent, PendingIntentFlags.Immutable);

        // var notificationManager = context.GetSystemService(Context.NotificationService).JavaCast<NotificationManager>();
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

        // notificationManager!.Notify(NotificationId, notification);

        StartForeground(NotificationId, notification);
        return StartCommandResult.Sticky;
    }
}
