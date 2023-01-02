using ZXing.Net.Maui;

namespace MobileClient;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            barcodeResult.Text = $"Result: {e.Results[0].Value} {e.Results[0].Format}";
        });
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        barcodeResult.Text = "Result:";
    }
}

