using Microsoft.Extensions.Logging;
using ContactMaster.Blazor.Services;
using Business.Interfaces;
using Business.Services;
using Business.Factories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace ContactMaster.Blazor
{

    // Konfigurerar och bygger upp .NET MAUI-applikationen.
    public static class MauiProgram
    {

        // Skapar och returnerar en konfigurerad instans av MAUI-applikationen.
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });


            // Lägger till stöd för Blazor i MAUI.
            builder.Services.AddMauiBlazorWebView();

            // Registrerar tjänster relaterade till affärslogik (Business)
            builder.Services.AddSingleton<IContactRepository, FileContactRepository>();
            builder.Services.AddSingleton<IContactService, ContactService>();
            builder.Services.AddSingleton<IContactFactory, ContactFactory>();

            // Registrerar tjänster för Blazor-funktionalitet
            builder.Services.AddSingleton<ThemeService>();
            builder.Services.AddSingleton<NotificationService>();



#if DEBUG
            // Aktiverar utvecklingsverktyg för Blazor i debug-läge.
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            // Bygger och returnerar den färdiga MAUI-applikationen.
            return builder.Build();
        }
    }
}