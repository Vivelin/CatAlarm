using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;

using Java.Util;

using MauiCatAlarm.Platforms.Android;

namespace MauiCatAlarm.Services;

public partial class AlarmService
{
    private AlarmManager AlarmManager;
    private PendingIntent? _pendingIntent;
    private TimeSpan? _scheduledTime;

    public AlarmService()
    {
        AlarmManager = Android.App.Application.Context.GetSystemService(Context.AlarmService).JavaCast<AlarmManager>()
            ?? throw new Exception("Failed to get AlarmManager");
    }

    public partial bool IsSet()
    {
        return _pendingIntent != null;
    }

    public partial TimeSpan? GetScheduledTime()
    {
        return _scheduledTime;
    }

    public partial void DeleteAlarm()
    {
        if (_pendingIntent == null)
        {
            throw new InvalidOperationException("Alarm not set");
        }

        AlarmManager.Cancel(_pendingIntent);
        Log.Info("AlarmService", "Alarm cancelled");
        _pendingIntent = null;
        _scheduledTime = null;
    }

    public partial void SetAlarm(TimeSpan startTime)
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(31) && !AlarmManager.CanScheduleExactAlarms())
        {
            throw new InvalidOperationException("Unable to schedule exact alarms");
        }

        var startTimeInMillis = ConvertToMillis(startTime);
        var intent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
        intent.SetFlags(ActivityFlags.ReceiverForeground);
        intent.PutExtra("triggerTime", startTimeInMillis);

        _pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, PendingIntentFlags.Immutable);
        if (_pendingIntent == null)
        {
            throw new Exception("Failed to get PendingIntent");
        }

        AlarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, startTimeInMillis, _pendingIntent);
        Log.Info("AlarmService", $"Alarm set for {startTime:t} exactly");

        _scheduledTime = startTime;
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
}
