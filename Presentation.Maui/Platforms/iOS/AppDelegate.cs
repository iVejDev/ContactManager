using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Presentation.Maui.Platforms.iOS  // Ändra från MAUI till Maui
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp()
        {
            return MauiProgram.CreateMauiApp();
        }
    }
}