namespace MauiWorkshop;

public interface INavigationService
{
    void SetMainPage<TViewModel>(App app);
    Task PushAsync<T>(object parameter);
}
