namespace MauiWorkshop;

public partial class MainPage : FlyoutPage
{
	public MainPage()
	{
		InitializeComponent();
    }

    void About_Clicked(System.Object sender, System.EventArgs e)
    {
        this.Detail = new NavigationPage(new AboutPage());
    }

    void Characters_Clicked(System.Object sender, System.EventArgs e)
    {
        this.Detail = new NavigationPage(new CharactersPage());
    }
}
