using MauiCatAlarm.Services;

namespace MauiCatAlarm;

public partial class AlarmPage : ContentPage
{
    private readonly AlarmService _alarmService;
    private readonly IChallengeFactory _challengeFactory;

    public AlarmPage(AlarmService alarmService, IChallengeFactory challengeFactory)
    {
        InitializeComponent();
        _alarmService = alarmService;
        _challengeFactory = challengeFactory;

        Challenge = challengeFactory.CreateChallenge();
        OnPropertyChanged(nameof(Challenge));
    }

    public Challenge Challenge { get; private set; }

    public bool ValidateAndDismissAlarm()
    {
        if (Challenge.Validate(ChallengeEntry.Text))
        {
            _alarmService.DismissAlarm();
            Application.Current!.CloseWindow(Window);
            return true;
        }

        return false;
    }

    private void StopAlarmButton_Clicked(object sender, EventArgs e)
    {
        ValidateAndDismissAlarm();
    }

    private void NewChallengeButton_Clicked(object sender, EventArgs e)
    {
        Challenge = _challengeFactory.CreateChallenge();
        OnPropertyChanged(nameof(Challenge));
    }

    private void ChallengeEntry_Completed(object sender, EventArgs e)
    {
        ValidateAndDismissAlarm();
    }
}
