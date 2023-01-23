using System.Net.Http.Headers;

namespace MobileClient.Pages;

public partial class DetailsPage : ContentPage
{
	public DetailsPage()
	{
		InitializeComponent();
	}

    private async void ShowDetailById_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(DetailIdEntry.Text, out int id))
        {
            var detail = await GetDetailAsyncById(id);

            DetailsByIdLabel.Text = detail;
        }
        else
        {
            await DisplayAlert("Error", "Неверный Id", "OK");
        }    
    }

    private async Task<string> GetDetailAsyncById(int id)
    {
        using var client = Resolver.ServiceProvider.GetRequiredService<HttpClient>();

        using var request = new HttpRequestMessage(HttpMethod.Get, $"{ApplicationConstants.ManufacturingApi}/api/Detail/1");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Token);

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}