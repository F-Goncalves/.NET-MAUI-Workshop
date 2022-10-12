using System.Diagnostics;
using MauiWorkshop.Navigation;

namespace MauiWorkshop;

//reference: https://blog.pieeatingninjas.be/2022/02/11/setting-up-a-basic-mvvm-architecture-in-net-maui/
public class NavigationService : INavigationService, IFlyoutNavigationService
{
    public static Dictionary<Type, Type> ViewModelPageMapping = new Dictionary<Type, Type>();

    readonly IServiceProvider _services;

    private FlyoutPage _flyoutPage;

    protected INavigation Navigation
    {
        get
        {
            if (_flyoutPage != null)
                return _flyoutPage.Detail.Navigation;

            INavigation? navigation = Application.Current?.MainPage?.Navigation;

            if (navigation is not null)
                return navigation;
            else
            {
                //This is not good!
                if (Debugger.IsAttached)
                    Debugger.Break();

                throw new Exception();
            }
        }
    }

    public NavigationService(IServiceProvider services) => _services = services;

    public Task PushAsync<TViewModel>(object parameter)
    {
        if (!ViewModelPageMapping.TryGetValue(typeof(TViewModel), out var pageType))
            throw new Exception($"{typeof(TViewModel)} was not registered");

        var viewModel = _services.GetService(typeof(TViewModel));

        if (viewModel is IInitialize initialize)
            initialize.Initialize(parameter);

        var page = (Page)_services.GetService(pageType);

        page.BindingContext = viewModel;

        return Navigation.PushAsync(page);
    }

    public void SetMainPage<TViewModel>(App app)
    {
        if (!ViewModelPageMapping.TryGetValue(typeof(TViewModel), out var pageType))
            throw new Exception($"{typeof(TViewModel)} was not registered");

        var viewModel = _services.GetService(typeof(TViewModel));
        var page = (Page)_services.GetService(pageType);

        page.BindingContext = viewModel;

        app.MainPage = page;
    }

    public void SetFlyoutMainPage<TFlyoutPageViewModel,TViewModel>(App app, bool wrapInNavigationPage)
    {
        if (!ViewModelPageMapping.TryGetValue(typeof(TFlyoutPageViewModel), out var flyoutPageType))
            throw new Exception($"{typeof(TFlyoutPageViewModel)} was not registered");

        if (!flyoutPageType.IsSubclassOf(typeof(FlyoutPage)))
            throw new Exception($"Page registered for {typeof(TFlyoutPageViewModel)} is not a FlyoutPage");

        if (!ViewModelPageMapping.TryGetValue(typeof(TViewModel), out var pageType))
            throw new Exception($"{typeof(TViewModel)} was not registered");

        var flyoutPageViewModel = _services.GetService(typeof(TFlyoutPageViewModel));
        var flyoutPage = (FlyoutPage)_services.GetService(flyoutPageType);
        flyoutPage.BindingContext = flyoutPageViewModel;

        var viewModel = _services.GetService(typeof(TViewModel));
        var page = (Page)_services.GetService(pageType);        
        page.BindingContext = viewModel;

        if (wrapInNavigationPage)
            page = new NavigationPage(page);

        flyoutPage.Detail = page;

        _flyoutPage = flyoutPage;

        app.MainPage = flyoutPage;
    }

    public void SetDetailPage<TViewModel>(bool wrapInNavigationPage)
    {
        SetDetailPage(typeof(TViewModel), wrapInNavigationPage);
    }

    public void SetDetailPage(Type viewModelType, bool wrapInNavigationPage)
    {
        if (_flyoutPage == null)
            throw new Exception("No FlyoutPage configured. Call SetFlyoutMainPage for setup.");

        if (!ViewModelPageMapping.TryGetValue(viewModelType, out var pageType))
            throw new Exception($"{viewModelType} was not registered");

        var viewModel = _services.GetService(viewModelType);
        var page = (Page)_services.GetService(pageType);

        page.BindingContext = viewModel;

        if (wrapInNavigationPage)
            page = new NavigationPage(page);

        _flyoutPage.Detail = page;
    }
}
