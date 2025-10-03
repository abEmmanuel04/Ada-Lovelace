using System;
using HomeTaskManager.Pages;

namespace HomeTaskManager;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    async void OnNavigateToRateEmotions(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RateEmotionsPage));
    }

    async void OnNavigateToGroupGoals(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GroupGoalsPage));
    }

    async void OnNavigateToAddGroup(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddGroupPage));
    }
}
