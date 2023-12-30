using MauiCatAlarm.Services;
using MauiCatAlarm.ViewModels;

namespace MauiCatAlarm;

public partial class AlarmPage : ContentPage
{
    public AlarmPage(AlarmViewModel alarmViewModel)
    {
        BindingContext = alarmViewModel;
        InitializeComponent();
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();

        ((AlarmViewModel)BindingContext).Window = Window;
    }
}
