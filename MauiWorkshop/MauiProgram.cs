using System.Diagnostics;
using MauiWorkshop.DisneyApi;
using MauiWorkshop.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiWorkshop;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("NewWaltDisneyFontRegular-BPen.ttf", "NewWaltDisneyFontRegular");
			});

		builder.Services.AddSingleton<IDisneyApiClient, DisneyApiClient>();

		builder.Services.AddSingleton<NavigationService>();
		builder.Services.AddSingleton<INavigationService>(sp => sp.GetRequiredService<NavigationService>());
		builder.Services.AddSingleton<IFlyoutNavigationService>(sp => sp.GetRequiredService<NavigationService>());

		builder.Services.AddViewModelMapping<MainPage, MainPageViewModel>();
		builder.Services.AddViewModelMapping<AboutPage, AboutPageViewModel>();
		builder.Services.AddViewModelMapping<CharactersPage, CharactersPageViewModel>();
		builder.Services.AddViewModelMapping<CharacterDetailsPage, CharacterDetailsPageViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}