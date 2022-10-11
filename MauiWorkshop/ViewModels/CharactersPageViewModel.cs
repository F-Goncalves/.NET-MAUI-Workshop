using System;
using System.Collections.ObjectModel;
using MauiWorkshop.DisneyApi;

namespace MauiWorkshop.ViewModels;

public class CharactersPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IDispatcher _dispatcher;
    private readonly IDisneyApiClient _disneyApiClient;

    private int _nextPage = 1;
    private bool _isNew = true;
    private int _lastLoadedPage;
    private Character _selectedCharacter;

    public Character SelectedCharacter
    {
        get => _selectedCharacter;
        set => SetProperty(ref _selectedCharacter, value);
    }

    public ObservableCollection<Character> Characters { get; }

    public Command SelectionChangedCommand { get; }
    public Command LoadMoreCharactersCommand { get; }

    public CharactersPageViewModel(INavigationService navigationService, IDispatcher dispatcher, IDisneyApiClient disneyApiClient)
	{
        _navigationService = navigationService;
        _dispatcher = dispatcher;
        _disneyApiClient = disneyApiClient;

        Characters = new ObservableCollection<Character>();

        SelectionChangedCommand = new Command(OnSelectionChanged);
        LoadMoreCharactersCommand = new Command(LoadCharacters);
    }

    public void OnAppearing()
    {
        LoadCharacters();

        _isNew = false;
    }

    private void OnCharactersLoaded(Task<CharactersResponse> loadTask, bool clear = false)
    {
        if (loadTask.IsFaulted)
        {
            Console.WriteLine($"Load characters failed: {loadTask.Exception.Message}");
            return;
        }

        var response = loadTask.Result;

        if (response == null)
        {
            Console.WriteLine($"Load characters failed: Response is null");
            return;
        }

        _nextPage++;

        _dispatcher.Dispatch(() =>
        {
            if (clear)
                Characters.Clear();

            foreach (var character in response.Data)
            {
                Characters.Add(character);
            }
        });
    }

    private void OnSelectionChanged()
    {
        if (SelectedCharacter == null)
            return;

        _navigationService.PushAsync<CharacterDetailsPageViewModel>(SelectedCharacter).ContinueWith(t => _dispatcher.Dispatch(() => SelectedCharacter = null));
    }

    private void LoadCharacters()
    {
        //Command might be executed multiple times for same reached threshold, let's not query the same page multiple times
        if (_nextPage <= _lastLoadedPage)
            return;

        _lastLoadedPage = _nextPage;

        _disneyApiClient.GetCharacters(_nextPage).ContinueWith(t => OnCharactersLoaded(t, _isNew));
    }
}

