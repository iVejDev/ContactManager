using Microsoft.Extensions.Logging;
using Business.Services;
using Business.Interfaces;
using Business.Factories;
using Presentation.Maui.ViewModels;
using Presentation.Maui.Views;

namespace Presentation.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registrera Business Services
            builder.Services.AddSingleton<IContactRepository, FileContactRepository>();
            builder.Services.AddSingleton<IContactService, ContactService>();
            builder.Services.AddSingleton<IContactFactory, ContactFactory>();

            // Registrera Views
            builder.Services.AddTransient<ContactListPage>();
            builder.Services.AddTransient<CreateContactPage>();
            builder.Services.AddTransient<EditContactPage>();

            // Registrera ViewModels
            builder.Services.AddTransient<ContactListViewModel>();
            builder.Services.AddTransient<CreateContactViewModel>();
            builder.Services.AddTransient<EditContactViewModel>();

            return builder.Build();
        }
    }
}