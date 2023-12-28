using MauiCatAlarm.Services;

using Microsoft.Extensions.Logging;

namespace MauiCatAlarm;

public partial class MainPage : ContentPage
{
    private readonly AlarmService _alarmService;

    public MainPage(AlarmService alarmService)
    {
        InitializeComponent();

        var scheduledTime = alarmService.GetScheduledTime();
        AlarmStartTimePicker.Time = scheduledTime ?? new TimeSpan(9, 0, 0);
        AlarmEndTimePicker.Time = scheduledTime?.Add(TimeSpan.FromMinutes(10)) ?? new TimeSpan(9, 30, 0);

        UpdateTime();
        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), UpdateTime);
        _alarmService = alarmService;
    }

    public bool IsAlarmSet => _alarmService.IsSet();

    public bool IsAlarmUnset => !IsAlarmSet;

    private bool UpdateTime()
    {
        TimeLabel.Text = DateTime.Now.ToString("T");
        return true;
    }

    private void SetAlarmButton_Clicked(object sender, EventArgs e)
    {
        if (_alarmService.IsSet())
        {
            _alarmService.DeleteAlarm();
            SetAlarmButton.Text = "Set alarm";
            OnPropertyChanged(nameof(IsAlarmSet));
            OnPropertyChanged(nameof(IsAlarmUnset));
        }
        else
        {
            _alarmService.SetAlarm(AlarmStartTimePicker.Time, AlarmEndTimePicker.Time, () => false);

            SetAlarmButton.Text = "Turn off";
            AlarmLabel.Text = FormatAlarmText();
            OnPropertyChanged(nameof(IsAlarmSet));
            OnPropertyChanged(nameof(IsAlarmUnset));
        }
    }

    private string FormatAlarmText()
    {
        var startTime = DateTime.Today.Add(AlarmStartTimePicker.Time);
        var endTime = DateTime.Today.Add(AlarmEndTimePicker.Time);
        return $"You'll be woken up between {startTime:t} and {endTime:t}.";
    }
}
