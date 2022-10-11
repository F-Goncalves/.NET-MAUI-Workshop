namespace MauiWorkshop;

public interface IFlyoutNavigationService
{
    void SetFlyoutMainPage<TFlyoutPageViewModel, TViewModel>(App app, bool wrapInNavigationPage);
    void SetDetailPage<TViewModel>(bool wrapInNavigationPage);
    void SetDetailPage(Type viewModelType, bool wrapInNavigationPage);
}