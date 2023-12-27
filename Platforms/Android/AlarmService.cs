using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;

using Java.Util;

using MauiCatAlarm.Platforms.Android;

namespace MauiCatAlarm.Services;

public partial class AlarmService
{
    private PendingIntent? _pendingIntent;

    public partial bool IsSet()
    {
        return _pendingIntent != null;
    }

    public partial void DeleteAlarm()
    {
        if (_pendingIntent == null)
        {
            throw new InvalidOperationException("Alarm not set");
        }

        var alarmManager = Android.App.Application.Context.GetSystemService(Context.AlarmService).JavaCast<AlarmManager>()
            ?? throw new Exception("Failed to get AlarmManager");

        alarmManager.Cancel(_pendingIntent);
        _pendingIntent = null;
    }

    public partial void SetAlarm(TimeSpan startTime, TimeSpan endTime, Func<bool> callback)
    {
        if (_pendingIntent != null)
        {
            throw new InvalidOperationException("Alarm already set");
        }

        var alarmManager = Android.App.Application.Context.GetSystemService(Context.AlarmService).JavaCast<AlarmManager>()
            ?? throw new Exception("Failed to get AlarmManager");

        if (OperatingSystem.IsAndroidVersionAtLeast(31) && !alarmManager.CanScheduleExactAlarms())
        {
            throw new InvalidOperationException("Unable to schedule exact alarms");
        }

        var intent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
        _pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, 0);
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
            alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, calendar.TimeInMillis, _pendingIntent);
        }
        else
        {
            alarmManager.SetExact(AlarmType.RtcWakeup, calendar.TimeInMillis, _pendingIntent);
        }
    }
}
