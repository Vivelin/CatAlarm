using System.ComponentModel;

using MauiCatAlarm.Platforms.Android;
using MauiCatAlarm.Services;
using MauiCatAlarm.Services.ViewModels;

namespace MauiCatAlarm;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel mainPageViewModel)
    {
        BindingContext = mainPageViewModel;

        InitializeComponent();
    }
}
