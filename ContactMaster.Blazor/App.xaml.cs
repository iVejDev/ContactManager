namespace ContactMaster.Blazor
{

    // Huvudklassen för applikationen, som ärvs från .NET MAUI:s Application-klass.
    public partial class App : Application
    {
        // Konstruktor för att initiera applikationen och dess komponenter.
        public App()
        {
            InitializeComponent();
        }

        // Skapar och returnerar huvudfönstret för applikationen
        protected override Window CreateWindow(IActivationState? activationState)
        {
            
            // Skapar ett nytt fönster som laddar MainPage och sätter titeln.
            return new Window(new MainPage()) { Title = "ContactMaster.Blazor" };
        }
    }
}
