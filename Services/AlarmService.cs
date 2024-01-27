namespace MauiCatAlarm.Services;

public partial class AlarmService
{
    public const int RingtonePickerRequestCode = 8008;

    public event EventHandler? ScheduledTimeChanged;

    public event EventHandler? IsEnabledChanged;

    public event EventHandler? RingtoneChanged;

    public partial void SetAlarm(TimeSpan startTime);

    public partial bool IsSet();

    public partial bool IsEnabled();

    public partial void DeleteAlarm();

    public partial void DismissAlarm();

    public partial void EnsureAlarmIsSetIfEnabled();

    public partial TimeSpan? GetScheduledTime();

    public partial void SetAlarmRingtone(string name, string filePath);

    public partial void PickAlarmRingtone();

    public partial void SetDefaultAlarmRingtone();
    
    public partial string GetAlarmRingtoneName();

    public partial string? GetAlarmRingtone();

    protected virtual void OnScheduledTimeChanged(object sender, EventArgs e)
    {
        ScheduledTimeChanged?.Invoke(sender, e);
    }

    protected virtual void OnIsEnabledChanged(object sender, EventArgs e)
    {
        IsEnabledChanged?.Invoke(sender, e);
    }

    protected virtual void OnRingtoneChanged(object sender, EventArgs e)
    {
        RingtoneChanged?.Invoke(sender, e);
    }
}
