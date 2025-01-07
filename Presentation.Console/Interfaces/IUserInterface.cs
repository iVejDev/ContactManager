
/*                                    
                                   Vad gör IUserInterface?

Syfte:
Gränssnittet definierar en standard för hur användargränssnittet ska fungera i konsolapplikationen.
Det separerar gränssnittets logik från resten av applikationen, vilket gör det enkelt att byta ut eller ändra gränssnittet.

Ansvar:
Tillhandahåller metoder för att:
Visa menyer och meddelanden.
Ta emot inmatning från användaren.
Hantera kontakter (visa, skapa och uppdatera).

Varför använda IUserInterface?
Gör användargränssnittet utbytbart och testbart.
Förbättrar kodens struktur genom att separera användarinteraktion från affärslogiken.

*/

namespace Presentation.Console.Interfaces
{
    public interface IUserInterface
    {
        // Visar huvudmenyn för användaren.
        void DisplayMainMenu();
        string GetUserInput();
        void Clear();
        // Visar detaljer om en kontakt.
        void DisplayMessage(string message);
        void WaitForKeyPress();
        // Hämtar detaljer för en ny kontakt från användaren.
        void DisplayContact(Business.Models.Contact contact);
        // Uppdaterar detaljer för en befintlig kontakt.
        Business.Models.Contact GetContactDetails();
        Business.Models.Contact UpdateContactDetails(Business.Models.Contact existingContact);
    }
}