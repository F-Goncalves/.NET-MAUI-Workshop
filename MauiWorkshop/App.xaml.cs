using MauiWorkshop.ViewModels;

namespace MauiWorkshop;

public partial class App : Application
{
	public App(IFlyoutNavigationService flyoutNavigationService)
	{
		InitializeComponent();

		flyoutNavigationService.SetFlyoutMainPage<MainPageViewModel, CharactersPageViewModel>(this, true);
	}
}