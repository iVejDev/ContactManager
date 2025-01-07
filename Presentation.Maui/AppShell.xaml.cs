namespace Presentation.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrera routes för navigation
            Routing.RegisterRoute("CreateContactPage", typeof(Views.CreateContactPage));
            Routing.RegisterRoute("EditContactPage", typeof(Views.EditContactPage));
        }
    }
}