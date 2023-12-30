using System.ComponentModel;

using MauiCatAlarm.Platforms.Android;
using MauiCatAlarm.Services;
using MauiCatAlarm.ViewModels;

namespace MauiCatAlarm;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel mainPageViewModel)
    {
        BindingContext = mainPageViewModel;

        InitializeComponent();
    }
}
