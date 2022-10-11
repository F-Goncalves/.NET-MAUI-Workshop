using System;
using MauiWorkshop.DisneyApi;
using MauiWorkshop.Navigation;

namespace MauiWorkshop.ViewModels;

public class CharacterDetailsPageViewModel : BaseViewModel, IInitialize
{
	private Character _character;

	public Character Character
	{
		get => _character;
		set => SetProperty(ref _character, value);
	}

	public CharacterDetailsPageViewModel()
	{
	}

    public void Initialize(object parameter)
    {
		if (!(parameter is Character character))
			throw new Exception("Parameter should be of type Character");

		Character = character;
    }
}

