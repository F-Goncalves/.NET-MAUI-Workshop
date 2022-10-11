namespace MauiWorkshop;

public static class NavigationServiceExtensions
{
    public static void AddViewModelMapping<TView, TViewModel>(this IServiceCollection serviceCollection) where TView : Page
    {
        serviceCollection.AddTransient(typeof(TView));
        serviceCollection.AddTransient(typeof(TViewModel));

        NavigationService.ViewModelPageMapping[typeof(TViewModel)] = typeof(TView);
    }
}