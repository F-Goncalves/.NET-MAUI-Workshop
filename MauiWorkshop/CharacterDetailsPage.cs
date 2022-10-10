using System.Collections.ObjectModel;
using MauiWorkshop.DisneyApi;

namespace MauiWorkshop;

public partial class CharacterDetailsPage : ContentPage
{
	public CharacterDetailsPage(Character character)
	{
		InitializeComponent();

		BindingContext = character;
	}
}


