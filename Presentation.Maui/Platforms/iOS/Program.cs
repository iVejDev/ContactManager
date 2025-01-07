using UIKit;
using Foundation;

namespace Presentation.Maui.Platforms.iOS
{
    public static class Program
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            UIApplication.Main(args, null, typeof(AppDelegate));
        }
    }
}