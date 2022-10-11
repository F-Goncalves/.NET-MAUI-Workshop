using System.Collections.ObjectModel;
using MauiWorkshop.DisneyApi;
using MauiWorkshop.ViewModels;

namespace MauiWorkshop;

public partial class CharactersPage : ContentPage
{
	private CharactersPageViewModel _viewModel;

	public CharactersPage()
	{
		InitializeComponent();
	}

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (BindingContext == null && _viewModel != null)
            _viewModel = null;
        else if (BindingContext != null && _viewModel == null)
            _viewModel = (CharactersPageViewModel)BindingContext;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

		_viewModel.OnAppearing();
    }
}


