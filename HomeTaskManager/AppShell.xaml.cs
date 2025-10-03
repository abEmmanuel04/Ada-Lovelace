using HomeTaskManager.Pages;

namespace HomeTaskManager;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(RateEmotionsPage), typeof(RateEmotionsPage));
		Routing.RegisterRoute(nameof(GroupGoalsPage), typeof(GroupGoalsPage));
		Routing.RegisterRoute(nameof(AddGroupPage), typeof(AddGroupPage));
	}
}
