using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace HomeTaskManager.Pages;

public partial class RateEmotionsPage : ContentPage
{
    public ObservableCollection<EmotionOption> EmotionOptions { get; } = new();

    EmotionOption? _selectedEmotion;

    public EmotionOption? SelectedEmotion
    {
        get => _selectedEmotion;
        set
        {
            if (_selectedEmotion == value)
            {
                return;
            }

            _selectedEmotion = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasSelection));
        }
    }

    public bool HasSelection => SelectedEmotion is not null;

    public ICommand SelectEmotionCommand { get; }
    public ICommand ConfirmSelectionCommand { get; }

    public RateEmotionsPage()
    {
        InitializeComponent();

        SelectEmotionCommand = new Command<EmotionOption>(OnSelectEmotion);
        ConfirmSelectionCommand = new Command(OnConfirmSelection, () => HasSelection);

        BindingContext = this;

        EmotionOptions.Add(new EmotionOption("Joyful", "happy_emoji.png", "Feeling upbeat and energized."));
        EmotionOptions.Add(new EmotionOption("Content", "semi_happy_emoji.png", "Calm and content with how things are going."));
        EmotionOptions.Add(new EmotionOption("Meh", "neutral_emoji.png", "Neutral mood—neither high nor low."));
        EmotionOptions.Add(new EmotionOption("Stressed", "semi_sad_emoji.png", "Things feel a little overwhelming right now."));
        EmotionOptions.Add(new EmotionOption("Down", "sad_emoji.png", "Feeling low and needing some support."));
    }

    void OnSelectEmotion(EmotionOption? option)
    {
        if (option is null)
        {
            return;
        }

        foreach (var emotion in EmotionOptions)
        {
            emotion.IsSelected = emotion == option;
        }

        SelectedEmotion = option;
        ((Command)ConfirmSelectionCommand).ChangeCanExecute();
    }

    async void OnConfirmSelection()
    {
        if (SelectedEmotion is null)
        {
            return;
        }

        await DisplayAlert("Response saved", $"Thanks! We'll factor in that you're feeling {SelectedEmotion.Name.ToLowerInvariant()}.", "OK");
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

    public class EmotionOption : INotifyPropertyChanged
    {
        bool _isSelected;

        public EmotionOption(string name, string icon, string description)
        {
            Name = name;
            Icon = icon;
            Description = description;
        }

        public string Name { get; }
        public string Icon { get; }
        public string Description { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value)
                {
                    return;
                }

                _isSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
