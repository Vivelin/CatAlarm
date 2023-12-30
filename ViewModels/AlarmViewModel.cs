using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MauiCatAlarm.Services;

namespace MauiCatAlarm.ViewModels;

public class AlarmViewModel : ObservableObject
{
    private readonly AlarmService _alarmService;
    private readonly IChallengeFactory _challengeFactory;
    private readonly Func<MainPage> _mainPageFactory;

    private Challenge _challenge;
    private string _challengeEntryText = "";

    public AlarmViewModel(
        AlarmService alarmService,
        IChallengeFactory challengeFactory,
        Func<MainPage> mainPageFactory)
    {
        _alarmService = alarmService;
        _challengeFactory = challengeFactory;
        _mainPageFactory = mainPageFactory;

        _challenge = _challengeFactory.CreateChallenge();
        DismissAlarmCommand = new RelayCommand(DismissAlarm, CanDismissAlarm);
        NewChallengeCommand = new RelayCommand(NewChallenge);
    }

    public Challenge Challenge
    {
        get => _challenge;
        private set => SetProperty(ref _challenge, value);
    }

    public string ChallengeEntryText
    {
        get => _challengeEntryText;
        set
        {
            if (SetProperty(ref _challengeEntryText, value))
                ((IRelayCommand)DismissAlarmCommand).NotifyCanExecuteChanged();
        }
    }

    public Window? Window { get; set; }

    public ICommand DismissAlarmCommand { get; }

    public ICommand NewChallengeCommand { get; }

    private void NewChallenge()
    {
        Challenge = _challengeFactory.CreateChallenge();
    }

    private bool CanDismissAlarm()
    {
        return Challenge.Validate(ChallengeEntryText);
    }

    private void DismissAlarm()
    {
        if (!Challenge.Validate(ChallengeEntryText)) return;

        _alarmService.DismissAlarm();

        if (App.Current.Windows.Count > 1 && Window != null)
        {
            App.Current.CloseWindow(Window);
        }
        else
        {
            App.Current.MainPage = _mainPageFactory();
        }
    }
}
