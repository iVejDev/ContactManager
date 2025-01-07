using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Presentation.Maui
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}