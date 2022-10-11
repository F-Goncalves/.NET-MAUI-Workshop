namespace MauiWorkshop;

public partial class ImageWithLoadingIndicator : ContentView
{
	public static BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(ImageWithLoadingIndicator), propertyChanged: OnImageSourcePropertyChanged);

    private static void OnImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
		((ImageWithLoadingIndicator)bindable).ImageSourceChanged();
    }

    public string ImageSource
	{
		get => (string)GetValue(ImageSourceProperty);
		set => SetValue(ImageSourceProperty, value);
	}

    public static BindableProperty AspectProperty = BindableProperty.Create(nameof(Aspect), typeof(Aspect), typeof(ImageWithLoadingIndicator), propertyChanged: OnAspectPropertyChanged);

    private static void OnAspectPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ImageWithLoadingIndicator)bindable).Image.Aspect = (Aspect)newValue;
    }

    public Aspect Aspect
    {
        get => (Aspect)GetValue(AspectProperty);
        set => SetValue(AspectProperty, value);
    }

    private bool _isLoading;

	public ImageWithLoadingIndicator()
	{
		InitializeComponent();

        Image.PropertyChanged += Image_PropertyChanged;
    }

    private void Image_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(Image.IsLoading))
            return;

        RefreshIsLoading(Image.IsLoading);
    }

    private void ImageSourceChanged()
    {
        RefreshIsLoading(true);
        Image.Source = null;
        Image.Source = ImageSource;
    }

    private void RefreshIsLoading(bool isLoading)
    {
        _isLoading = isLoading;
        ActivityIndicator.IsRunning = ActivityIndicator.IsVisible = isLoading;
    }
}
