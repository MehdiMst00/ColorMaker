namespace ColorMaker;

public partial class MainPage : ContentPage
{
    private readonly IToast toast;
    private string hexCode = "#000000";
    private bool isRandom;

    public MainPage()
    {
        InitializeComponent();
        toast = Toast.Make("Color copied", ToastDuration.Short);
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (isRandom)
            return;

        var red = RedSlider.Value;
        var green = GreenSlider.Value;
        var blue = BlueSlider.Value;

        var color = Color.FromRgb(red, green, blue);

        SetColor(color);
    }

    private void RandomButton_Clicked(object sender, EventArgs e)
    {
        isRandom = true;

        var random = new Random();

        var color = Color.FromRgb(
            random.Next(0, 256),
            random.Next(0, 256),
            random.Next(0, 256));

        SetColor(color);

        RedSlider.Value = color.Red;
        GreenSlider.Value = color.Green;
        BlueSlider.Value = color.Blue;

        isRandom = false;
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(hexCode);
        await toast.Show();
    }

    public void SetColor(Color color)
    {
#if ANDROID
        StatusBar.StatusBarColor = color;
#endif
        MainGrid.BackgroundColor = color;
        RandomButton.Background = color;
        hexCode = color.ToHex();
        HexLabel.Text = $"HEX Value: {hexCode}";
    }
}

