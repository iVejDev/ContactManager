# ContactManager


Kontaktlista - Blazor Applikation
Detta är en inlämningsuppgift där jag har skapat en kontaktlista-applikation som uppfyller kraven för både godkänt (G) och väl godkänt (VG). Applikationen är byggd med C# och Blazor och inkluderar funktionalitet för att hantera kontakter samt stöd för mörkt läge (Dark Mode).

Funktionalitet:

Godkänt (G)
Konsolapplikation: 
    En applikation byggd som en konsolapplikation med följande funktioner:
    *Lista alla kontakter.
    *Skapa en ny kontakt.
    *Spara kontakter i en .json-fil.
    *Läsa in kontakter från .json-fil vid start och vid uppdateringar.
    *Tillämpning av SOLID-principer.
    *Enhetstester för metoder som kan testas utan mockning.

Väl godkänt (VG)
GUI-applikation: En Blazor-applikation med följande funktioner:
    *En sida som listar alla kontakter med deras detaljer.
    *En sida för att skapa nya kontakter.
    *Möjlighet att redigera eller ta bort kontakter.
    *Kontakterna sparas i en .json-fil och laddas in vid start och uppdateringar.
    *Implementering av Dependency Injection.
    *Användning av flera design patterns, inklusive:
      **Service Pattern
      **Factory Pattern
    *Enhetstester med mockning där det behövs.
    *Dark Mode-funktionalitet som användaren kan växla mellan.

Tekniker och verktyg
    *Programmeringsspråk: C#
    *Framework: Blazor
    *Design Patterns: SOLID, Service Pattern, Factory Pattern
    *JSON-hantering: För att spara och läsa kontakter.
    *Dependency Injection: För att hantera tjänster och funktionalitet.

Testning: Enhetstester (med och utan mockning).
Dark Mode: Dynamisk växling mellan ljus och mörk design.



Viss kod i projektet har genererats med hjälp av AI (t.ex. ChatGPT). Detta anges i kommentarerna där det används. Koden har granskats, modifierats och kommenterats för att förklara dess funktion.

Installation och körning

1. Konsolapplikation:
    *Kör .exe-filen från konsolprojektet eller bygg projektet från källkoden i Visual Studio.

2.Blazor-applikation:
    *Öppna projektet i Visual Studio.
    *Kör applikationen genom att trycka på F5 eller starta den i debug-läge.
    *Dark Mode kan aktiveras/deaktiveras via knappen i topphöger.

3. Skärmbilder
Ljus läge + Mörkt läge
