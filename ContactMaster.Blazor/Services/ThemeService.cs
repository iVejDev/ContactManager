using ContactMaster.Blazor.Models;
using System;

namespace ContactMaster.Blazor.Services
{
    public class ThemeService
    {
        private ThemePreference _themePreference = new();
        public event Action? OnThemeChanged;

        public ThemePreference GetCurrentTheme() => _themePreference;

        public void ToggleTheme()
        {
            _themePreference.CurrentTheme = _themePreference.CurrentTheme == Theme.Light ? Theme.Dark : Theme.Light;
            NotifyThemeChanged();
        }

        private void NotifyThemeChanged() => OnThemeChanged?.Invoke();
    }
}