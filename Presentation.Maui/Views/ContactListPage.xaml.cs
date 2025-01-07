using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Presentation.Maui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactListPage : ContentPage
    {
        public ContactListPage(ViewModels.ContactListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}