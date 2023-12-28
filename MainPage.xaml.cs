using MauiCatAlarm.Platforms.Android;
using MauiCatAlarm.Services;

using Microsoft.Extensions.Logging;

namespace MauiCatAlarm;

public partial class MainPage : ContentPage
{
    private readonly AlarmService _alarmService;

    public MainPage(AlarmService alarmService)
    {
        _alarmService = alarmService;

        InitializeComponent();

        var scheduledTime = alarmService.GetScheduledTime();
        AlarmStartTimePicker.Time = scheduledTime ?? new TimeSpan(9, 0, 0);

#if DEBUG
        AlarmStartTimePicker.Time = DateTime.Now.NextMinute().TimeOfDay;
#endif

        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            OnPropertyChanged(nameof(FormattedCurrentTime));
            return true;
        });
    }

    public bool IsAlarmSet => _alarmService.IsSet();

    public bool IsAlarmUnset => !IsAlarmSet;

    public string FormattedCurrentTime => DateTime.Now.ToString("T");

    private async void SetAlarmButton_Clicked(object sender, EventArgs e)
    {
        var status = await Permissions.CheckStatusAsync<PostNotificationsPermission>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<PostNotificationsPermission>();
            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Permission required", "An alarm app without permissions to show you alarms is pretty sad.", "OK");
                return;
            }
        }

        if (_alarmService.IsSet())
        {
            _alarmService.DeleteAlarm();
            SetAlarmButton.Text = "Set alarm";
            OnPropertyChanged(nameof(IsAlarmSet));
            OnPropertyChanged(nameof(IsAlarmUnset));
        }
        else
        {
            _alarmService.SetAlarm(AlarmStartTimePicker.Time);

            SetAlarmButton.Text = "Turn off";
            AlarmLabel.Text = FormatAlarmText();
            OnPropertyChanged(nameof(IsAlarmSet));
            OnPropertyChanged(nameof(IsAlarmUnset));
        }
    }

    private string FormatAlarmText()
    {
        var scheduledTime = _alarmService.GetScheduledTime();
        if (scheduledTime == null)
        {
            return $"Zzzzzzzz…";
        }

        var startTime = DateTime.Today.Add(scheduledTime.Value);
        return $"You'll be woken up at {startTime:t}.";
    }
}
