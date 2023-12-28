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
        var triggerTime = AlarmManager.NextAlarmClock?.TriggerTime;
        return triggerTime.HasValue ? TimeSpan.FromMilliseconds(triggerTime.Value) : null;
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
    }

    public partial void SetAlarm(TimeSpan startTime, TimeSpan endTime, Func<bool> callback)
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(31) && !AlarmManager.CanScheduleExactAlarms())
        {
            throw new InvalidOperationException("Unable to schedule exact alarms");
        }

        var intent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
        var flags = OperatingSystem.IsAndroidVersionAtLeast(23) ? PendingIntentFlags.Immutable : 0;
        _pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, flags);
        if (_pendingIntent == null)
        {
            throw new Exception("Failed to get PendingIntent");
        }

        //var startMillis = Java.Lang.JavaSystem.CurrentTimeMillis() + (long)startTime.TotalMilliseconds;
        //var lengthMillis = (long)(endTime - startTime).TotalMilliseconds;
        //alarmManager.SetWindow(AlarmType.RtcWakeup, startMillis, lengthMillis, alarmIntent!);

        var calendar = Calendar.Instance;
        calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
        calendar.Set(CalendarField.HourOfDay, startTime.Hours);
        calendar.Set(CalendarField.Minute, startTime.Minutes);
        calendar.Set(CalendarField.Second, startTime.Seconds);

        if (OperatingSystem.IsAndroidVersionAtLeast(23))
        {
            AlarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, calendar.TimeInMillis, _pendingIntent);
            Log.Info("AlarmService", $"Alarm set for {startTime:t} exactly");
        }
        else
        {
            AlarmManager.SetExact(AlarmType.RtcWakeup, calendar.TimeInMillis, _pendingIntent);
            Log.Info("AlarmService", $"Alarm set for {startTime:t}");
        }
    }
}
