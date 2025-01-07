namespace ContactMaster.Blazor.Models
{

    // En enum som representerar de tillgängliga temavalen (ljus och mörk).
    public enum Theme
    {
        Light,
        Dark
    }
    // En klass som hanterar användarens tema-inställningar och tillhörande logik.
    public class ThemePreference
    {
        public Theme CurrentTheme { get; set; } = Theme.Light;

        // Returnerar en CSS-klass baserat på det valda temat.
        // Om temat är "Dark", returneras "dark-mode"; annars "light-mode".
        public string GetThemeClass() => CurrentTheme == Theme.Dark ? "dark-mode" : "light-mode";
    }
}