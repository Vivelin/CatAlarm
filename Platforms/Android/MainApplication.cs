using Android.App;
using Android.Graphics;
using Android.Runtime;
using Android.Util;

namespace MauiCatAlarm;

[Application]
public class MainApplication : MauiApplication
{
    public const string ChannelId = "alarms";

    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override void OnCreate()
    {
        base.OnCreate();

        var channel = new NotificationChannel(ChannelId, "Alarms", NotificationImportance.High);
        var notificationManager = Context.GetSystemService(NotificationService).JavaCast<NotificationManager>();
        notificationManager!.CreateNotificationChannel(channel);
    }
}
