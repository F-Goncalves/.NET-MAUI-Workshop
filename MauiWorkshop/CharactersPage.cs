using System.Collections.ObjectModel;
using MauiWorkshop.DisneyApi;

namespace MauiWorkshop;

public partial class CharactersPage : ContentPage
{
	private readonly DisneyApiClient _apiClient;

	private int _nextPage = 1;
	private bool _isNew = true;
    private int _lastLoadedPage;

	public ObservableCollection<Character> Characters { get; }

	public CharactersPage()
	{
		InitializeComponent();

		_apiClient = new DisneyApiClient();

		Characters = new ObservableCollection<Character>();
		BindingContext = this;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

		_apiClient.GetCharacters(_nextPage).ContinueWith(t => OnCharactersLoaded(t, _isNew));

        _isNew = false;
    }

	private void OnCharactersLoaded(Task<CharactersResponse> loadTask, bool clear = false)
	{
		if(loadTask.IsFaulted)
		{
			Console.WriteLine($"Load characters failed: {loadTask.Exception.Message}");
			return;
		}

		var response = loadTask.Result;

		if(response == null)
		{
			Console.WriteLine($"Load characters failed: Response is null");
			return;
		}

		_nextPage++;

		Dispatcher.Dispatch(() =>
        {
			if (clear)
                Characters.Clear();

			foreach (var character in response.Data)
			{
                Characters.Add(character);
			}
        });
	}

    private void CharacterList_RemainingItemsThresholdReached(System.Object sender, System.EventArgs e)
    {
		//Event might be fired multiple times for same reached threshold, let's not query the same page multiple times
		if (_nextPage <= _lastLoadedPage)
			return;

		_lastLoadedPage = _nextPage;

		_apiClient.GetCharacters(_nextPage).ContinueWith(t => OnCharactersLoaded(t));
    }

    private void CharacterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if (!(e?.CurrentSelection?.Any() ?? false))
			return;

		var character = e.CurrentSelection[0] as Character;

		if (character == null)
			return;

		Navigation.PushAsync(new CharacterDetailsPage(character)).ContinueWith(t => CharacterList.SelectedItem = null);
    }
}


