using System;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;

namespace HomeTaskManager.Pages;

public partial class AddGroupPage : ContentPage
{
    readonly Random _random = new();

    public AddGroupPage()
    {
        InitializeComponent();

        CreateGroupCommand = new Command(async () => await OnCreateGroup());
        JoinGroupCommand = new Command(async () => await OnJoinGroup());
        CopyCodeCommand = new Command(async () => await OnCopyCode(), () => !string.IsNullOrEmpty(GeneratedCode));

        GeneratedCode = GenerateInviteCode();

        BindingContext = this;
    }

    public string GeneratedCode { get; private set; }

    public ICommand CreateGroupCommand { get; }
    public ICommand JoinGroupCommand { get; }
    public ICommand CopyCodeCommand { get; }

    async Task OnCreateGroup()
    {
        var name = GroupNameEntry.Text?.Trim();
        var focus = GroupFocusEditor.Text?.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlert("Missing info", "Give your group a name so everyone knows what it is.", "Got it");
            return;
        }

        GeneratedCode = GenerateInviteCode();
        OnPropertyChanged(nameof(GeneratedCode));
        ((Command)CopyCodeCommand).ChangeCanExecute();

        GroupNameEntry.Text = string.Empty;
        GroupFocusEditor.Text = string.Empty;

        var message = string.IsNullOrWhiteSpace(focus)
            ? $"Share this invite code with your teammates: {GeneratedCode}."
            : $"Share this invite code with your teammates: {GeneratedCode}.\nFocus: {focus}";

        await DisplayAlert("Group created", message, "Nice");
    }

    async Task OnJoinGroup()
    {
        var code = JoinCodeEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(code))
        {
            await DisplayAlert("Missing code", "Enter the invite code you received to join an existing group.", "OK");
            return;
        }

        JoinCodeEntry.Text = string.Empty;
        await DisplayAlert("Joined", $"You're in! We'll load the goals for group {code} next.", "Great");
    }

    async Task OnCopyCode()
    {
        if (string.IsNullOrEmpty(GeneratedCode))
        {
            return;
        }

        await Clipboard.SetTextAsync(GeneratedCode);
        await DisplayAlert("Copied", "Invite code copied to clipboard.", "OK");
    }

    string GenerateInviteCode()
    {
        const string alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        var builder = new StringBuilder(6);

        for (var i = 0; i < 6; i++)
        {
            var index = _random.Next(alphabet.Length);
            builder.Append(alphabet[index]);
        }

        return builder.ToString();
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
