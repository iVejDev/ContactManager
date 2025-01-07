using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Presentation.Maui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateContactPage : ContentPage
    {
        public CreateContactPage(ViewModels.CreateContactViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}