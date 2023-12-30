using MauiCatAlarm.Services;

namespace MauiCatAlarm;

public partial class AlarmPage : ContentPage
{
    private readonly AlarmService _alarmService;
    private readonly IChallengeFactory _challengeFactory;
    private readonly Func<MainPage> _mainPageFactory;

    public AlarmPage(AlarmService alarmService, IChallengeFactory challengeFactory, Func<MainPage> mainPageFactory)
    {
        InitializeComponent();
        _alarmService = alarmService;
        _challengeFactory = challengeFactory;
        _mainPageFactory = mainPageFactory;
        Challenge = challengeFactory.CreateChallenge();
        OnPropertyChanged(nameof(Challenge));
    }

    public Challenge Challenge { get; private set; }

    public bool ValidateAndDismissAlarm()
    {
        if (Challenge.Validate(ChallengeEntry.Text))
        {
            _alarmService.DismissAlarm();
            if (App.Current.Windows.Count > 1)
            {
                App.Current.CloseWindow(Window);
            }
            else
            {
                ((App)App.Current!).MainPage = _mainPageFactory();
            }
            return true;
        }

        ChallengeEntry.Text = "";
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
