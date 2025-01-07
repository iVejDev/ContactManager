using Business.Factories;
using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Console.Interfaces;
using Presentation.Console.Services;



/*
                                    Vad gör Program.cs?
Syfte:

Startar applikationen och konfigurerar alla tjänster som behövs.
Skapar en instans av huvudmenyn (MenuService) och kör den.
Ansvar:

Dependency Injection (DI):
Använder ServiceCollection för att hantera beroenden mellan komponenter.
Registrerar alla nödvändiga tjänster och deras implementationer.
Startar huvudlogiken via menuService.Run().


Varför använda Dependency Injection (DI)?
Flexibilitet:
Gör det enkelt att byta ut implementationer, t.ex. byta FileContactRepository mot en databasimplementation.

Testbarhet:
Underlättar att enhetstesta genom att använda mockade beroenden.

Minskar kopplingar:
Komponenter som MenuService behöver inte känna till hur ContactService eller ConsoleUserInterface skapas.
*/


namespace Presentation.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Skapar en samling tjänster för Dependency Injection (DI).
            // Konfigurerar alla tjänster som behövs för applikationen.
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Bygger en tjänsteleverantör för att lösa beroenden.
            // Hämtar instansen av IMenuService.
            var serviceProvider = services.BuildServiceProvider();
            var menuService = serviceProvider.GetRequiredService<IMenuService>();

            menuService.Run();
        }

        // Konfigurerar alla tjänster som används i applikationen.
        public static void ConfigureServices(IServiceCollection services)
        {
            // Registrerar ContactFactory som en singleton.
            services.AddSingleton<IContactFactory, ContactFactory>();
            // Registrerar FileContactRepository som hanterar datalagring.
            services.AddSingleton<IContactRepository, FileContactRepository>();
            // Registrerar ContactService för att hantera kontaktlogik.
            services.AddSingleton<IContactService, ContactService>();
            // Registrerar ConsoleUserInterface för att hantera användarens interaktion med konsolen.
            services.AddSingleton<IUserInterface, ConsoleUserInterface>();
            // Registrerar MenuService för att hantera menylogik.
            services.AddSingleton<IMenuService, MenuService>();
        }
    }
}