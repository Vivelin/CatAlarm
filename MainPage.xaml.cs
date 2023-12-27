namespace MauiCatAlarm;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        UpdateTime();
        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), UpdateTime);
    }

    private bool UpdateTime()
    {
        TimeLabel.Text = DateTime.Now.ToString("T");
        return true;
    }
}

