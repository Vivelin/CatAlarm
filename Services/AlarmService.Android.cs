using System.Globalization;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;

using Java.Util;

using MauiCatAlarm.Platforms.Android;

using Calendar = Java.Util.Calendar;

namespace MauiCatAlarm.Services;

public partial class AlarmService
{
    private readonly AlarmManager _alarmManager;
    private PendingIntent? _pendingIntent;

    public AlarmService()
    {
        _alarmManager = Platform.AppContext.GetSystemService(Context.AlarmService).JavaCast<AlarmManager>()
            ?? throw new Exception("Failed to get AlarmManager");
    }

    public partial bool IsSet()
    {
        return _pendingIntent != null;
    }

    public partial TimeSpan? GetScheduledTime()
    {
        var storedValue = Preferences.Default.Get<string?>("start_time", null);
        if (TimeSpan.TryParseExact(storedValue, "hh\\:mm", CultureInfo.InvariantCulture, out var timeSpan))
            return timeSpan;

        return null;
    }

    public partial void DeleteAlarm()
    {
        if (_pendingIntent == null)
        {
            throw new InvalidOperationException("Alarm not set");
        }

        _alarmManager.Cancel(_pendingIntent);
        Preferences.Default.Remove("start_time");
        Log.Info("AlarmService", "Alarm cancelled");
        _pendingIntent = null;
    }

    public partial void SetAlarm(TimeSpan startTime)
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(31) && !_alarmManager.CanScheduleExactAlarms())
        {
            throw new InvalidOperationException("Unable to schedule exact alarms");
        }

        var startTimeInMillis = ConvertToMillis(startTime);
        var intent = new Intent(Platform.AppContext, typeof(AlarmReceiver));
        intent.SetFlags(ActivityFlags.ReceiverForeground);
        intent.PutExtra("triggerTime", startTimeInMillis);

        _pendingIntent = PendingIntent.GetBroadcast(Platform.AppContext, 0, intent, PendingIntentFlags.Immutable);
        if (_pendingIntent == null)
        {
            throw new Exception("Failed to get PendingIntent");
        }

        _alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, startTimeInMillis, _pendingIntent);
        Preferences.Default.Set("start_time", startTime.ToString("hh\\:mm", CultureInfo.InvariantCulture));
        Log.Info("AlarmService", $"Alarm set for {startTime:t} exactly");
    }

    public partial void DismissAlarm()
    {
        var intent = new Intent(Platform.AppContext, typeof(ActiveAlarmService));
        Platform.AppContext.StopService(intent);
    }

    private static long ConvertToMillis(TimeSpan time)
    {
        var calendar = Calendar.Instance;
        calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
        calendar.Set(CalendarField.HourOfDay, time.Hours);
        calendar.Set(CalendarField.Minute, time.Minutes);
        calendar.Set(CalendarField.Second, time.Seconds);
        return calendar.TimeInMillis;
    }

    private static TimeSpan ConvertFromMillis(long millis)
    {
        var calendar = Calendar.Instance;
        calendar.TimeInMillis = millis;
        return new TimeSpan(
            calendar.Get(CalendarField.HourOfDay),
            calendar.Get(CalendarField.Minute),
            calendar.Get(CalendarField.Second));
    }

    private class ServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public void OnServiceConnected(ComponentName? name, IBinder? service)
        {
            throw new NotImplementedException();
        }

        public void OnServiceDisconnected(ComponentName? name)
        {
            throw new NotImplementedException();
        }
    }
}
