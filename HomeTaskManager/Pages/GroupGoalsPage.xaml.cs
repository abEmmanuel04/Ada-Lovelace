using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HomeTaskManager.Pages;

public partial class GroupGoalsPage : ContentPage
{
    public ObservableCollection<GroupGoalSection> Sections { get; } = new();

    public ICommand AddGoalCommand { get; }

    public GroupGoalsPage()
    {
        InitializeComponent();

        AddGoalCommand = new Command(OnAddGoal);

        BindingContext = this;

        Sections.Add(new GroupGoalSection(
            "Daily Habits",
            "Lightweight actions to keep everyone on track.",
            new List<GroupGoal>
            {
                new("Share your top win in chat", "Social Connection", 3, 5, 5),
                new("Post a group gratitude photo", "Social Connection", 1, 2, 10)
            }));

        Sections.Add(new GroupGoalSection(
            "Weekly Focus",
            "Goals that need collaboration throughout the week.",
            new List<GroupGoal>
            {
                new("Cook a healthy recipe together", "Nutritional Habits", 2, 3, 15),
                new("Buddy workout", "Physical Activity", 1, 2, 15)
            }));

        Sections.Add(new GroupGoalSection(
            "Completed",
            "Wins worth celebrating!",
            new List<GroupGoal>
            {
                new("Plan the sleep reset challenge", "Sleeping Habits", 2, 2, 20)
            }));
    }

    async void OnAddGoal()
    {
        await DisplayAlert("Coming soon", "Goal creation for groups will be available after we finish the data layer.", "OK");
    }

    async void OnBackClicked(object? sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
        {
            await Navigation.PopAsync();
            return;
        }

        await Shell.Current.GoToAsync("..", true);
    }
}

public class GroupGoalSection
{
    public GroupGoalSection(string title, string subtitle, IEnumerable<GroupGoal> goals)
    {
        Title = title;
        Subtitle = subtitle;
        Goals = new ObservableCollection<GroupGoal>(goals);
    }

    public string Title { get; }
    public string Subtitle { get; }
    public ObservableCollection<GroupGoal> Goals { get; }
}

public class GroupGoal
{
    public GroupGoal(string title, string category, int progressCurrent, int progressTarget, int points)
    {
        Title = title;
        Category = category;
        ProgressCurrent = progressCurrent;
        ProgressTarget = progressTarget;
        Points = points;
    }

    public string Title { get; }
    public string Category { get; }
    public int ProgressCurrent { get; }
    public int ProgressTarget { get; }
    public int Points { get; }

    public double Progress => ProgressTarget == 0 ? 0 : Math.Min(1, (double)ProgressCurrent / ProgressTarget);

    public string ProgressDisplay => $"{ProgressCurrent}/{ProgressTarget}";

    public string PointsDisplay => $"{Points} pts";
}
