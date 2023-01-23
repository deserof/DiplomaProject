using OpenIddict.Client;
using ZXing.Net.Maui;

namespace MobileClient.Pages;

public partial class QrLoginPage : ContentPage
{
	public QrLoginPage()
	{
		InitializeComponent();
	}

    private async void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            barcodeResult.Text = $"Result: {e.Results[0].Value} {e.Results[0].Format}";
        });

        //try
        //{
        //    TokenStorage.Token = await GetTokenAsync("qwe@gmail.com", "password1!Q");

        //    await Navigation.PushAsync(new DetailsPage());
        //}
        //catch (Exception ex)
        //{
        //    await DisplayAlert("Error", ex.Message, "OK");
        //}
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        barcodeResult.Text = "Result:";
    }

    private async Task<string> GetTokenAsync(string email, string password)
    {
        var service = Resolver.ServiceProvider.GetRequiredService<OpenIddictClientService>();

        var (response, _) = await service.AuthenticateWithPasswordAsync(
            issuer: new Uri(ApplicationConstants.ManufacturingApi, UriKind.Absolute),
            username: email,
            password: password);

        return response.AccessToken;
    }
}