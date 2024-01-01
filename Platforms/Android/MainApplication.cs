using Android.App;
using Android.Graphics;
using Android.Media;
using Android.Runtime;
using Android.Util;

using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace MauiCatAlarm;

[Application]
public class MainApplication : MauiApplication
{
    public const string ChannelId = "alarms";

    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }

    public override void OnCreate()
    {
        base.OnCreate();

        var channel = new NotificationChannel(ChannelId, "Alarms", NotificationImportance.High);
        channel.SetSound(null, null);
        channel.SetBypassDnd(true);
        var notificationManager = Context.GetSystemService(NotificationService).JavaCast<NotificationManager>();
        notificationManager!.CreateNotificationChannel(channel);
    }
}
