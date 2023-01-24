using MobileClient.Models;
using Newtonsoft.Json;
using OpenIddict.Client;
using ZXing.Net.Maui;

namespace MobileClient.Pages;

public partial class QrLoginPage : ContentPage
{
    public QrLoginPage()
    {
        InitializeComponent();
    }

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            barcodeResult.Text = e.Results[0].Value;
        });
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        barcodeResult.Text = string.Empty;
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

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(barcodeResult.Text))
            {
                var user = JsonConvert.DeserializeObject<UserLoginModel>(barcodeResult.Text);
                TokenStorage.Token = await GetTokenAsync(user.Email, user.Password);
                barcodeResult.Text = string.Empty;

                await Navigation.PushAsync(new DetailsPage());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}