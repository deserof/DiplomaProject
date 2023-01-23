using MobileClient.Pages;
using OpenIddict.Client;

namespace MobileClient;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            //TokenStorage.Token = await GetTokenAsync("qwe@gmail.com", "password1!Q");
            TokenStorage.Token = await GetTokenAsync(EmailEntry.Text, PasswordEntry.Text);

            await Navigation.PushAsync(new DetailsPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
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

    private async void QrCodeButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QrLoginPage());
    }
}
