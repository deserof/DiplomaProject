using Microsoft.IdentityModel.Logging;
using MobileClient.Extensions;
using OpenIddict.Client;
using ZXing.Net.Maui;

namespace MobileClient;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseBarcodeReader()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddOpenIddict()

            // Register the OpenIddict client components.
            .AddClient(options =>
            {
                // Allow grant_type=password to be negotiated.
                options.AllowPasswordFlow();

                // Disable token storage, which is not necessary for non-interactive flows like
                // grant_type=password, grant_type=client_credentials or grant_type=refresh_token.
                options.DisableTokenStorage();

                // Register the System.Net.Http integration and use the identity of the current
                // assembly as a more specific user agent, which can be useful when dealing with
                // providers that use the user agent as a way to throttle requests (e.g Reddit).
                options.UseSystemNetHttp()
                       .SetProductInformation(typeof(MauiProgram).Assembly);

                // Add a client registration without a client identifier/secret attached.
                options.AddRegistration(new OpenIddictClientRegistration
                {
                    Issuer = new Uri(ApplicationConstants.ManufacturingApi, UriKind.Absolute)
                });
            });

        IdentityModelEventSource.ShowPII = true;

        var app = builder.Build();

        app.UseResolver();

        return app;
	}
}
