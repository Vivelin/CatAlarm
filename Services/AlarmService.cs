namespace MauiCatAlarm.Services;

public partial class AlarmService
{
    public event EventHandler? ScheduledTimeChanged;

    public event EventHandler? IsEnabledChanged;

    public partial void SetAlarm(TimeSpan startTime);

    public partial bool IsSet();

    public partial bool IsEnabled();

    public partial void DeleteAlarm();

    public partial void DismissAlarm();

    public partial void EnsureAlarmIsSetIfEnabled();

    public partial TimeSpan? GetScheduledTime();

    protected virtual void OnScheduledTimeChanged(object sender, EventArgs e)
    {
        ScheduledTimeChanged?.Invoke(sender, e);
    }

    protected virtual void OnIsEnabledChanged(object sender, EventArgs e)
    {
        IsEnabledChanged?.Invoke(sender, e);
    }
}
