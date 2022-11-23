namespace MauiWorkshop;

public partial class RootFlyoutPage
{
	public RootFlyoutPage()
	{
		InitializeComponent();
	}

    void Characters_Clicked(System.Object sender, System.EventArgs e)
    {
        this.Detail = new NavigationPage(new MainPage());
    }

    void About_Clicked(System.Object sender, System.EventArgs e)
    {
        this.Detail = new NavigationPage(new MainPage());
    }
}
