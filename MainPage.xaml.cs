using MauiCatAlarm.Services;

using Microsoft.Extensions.Logging;

namespace MauiCatAlarm;

public partial class MainPage : ContentPage
{
    private readonly AlarmService _alarmService;

    public MainPage(AlarmService alarmService)
    {
        InitializeComponent();

        AlarmStartTimePicker.Time = new TimeSpan(9, 0, 0);
        AlarmEndTimePicker.Time = new TimeSpan(9, 30, 0);

        UpdateTime();
        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), UpdateTime);
        _alarmService = alarmService;
    }

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
            AlarmStartTimePicker.IsVisible = true;
            AlarmEndTimePicker.IsVisible = true;
            AlarmPickerLabel.IsVisible = true;
            AlarmLabel.IsVisible = false;
        }
        else
        {
            _alarmService.SetAlarm(AlarmStartTimePicker.Time, AlarmEndTimePicker.Time, () =>
            {
                Dispatcher.Dispatch(() =>
                {
                    DisplayAlert("Alarm", "Time to wake up!", "OK");
                });
                return true;
            });

            SetAlarmButton.Text = "Turn off";
            AlarmStartTimePicker.IsVisible = false;
            AlarmEndTimePicker.IsVisible = false;
            AlarmPickerLabel.IsVisible = false;
            AlarmLabel.Text = $"You'll be woken up between {AlarmStartTimePicker.Time} and {AlarmEndTimePicker.Time}.";
            AlarmLabel.IsVisible = true;
        }
    }
}
