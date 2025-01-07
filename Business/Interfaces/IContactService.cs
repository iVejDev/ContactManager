using Business.Models;

/* 
                                    IContactService
Ansvar:
Hanterar affärslogik (business logic) och använder IContactRepository som datakälla.
Abstraktionen mellan det som användaren gör och hur data hanteras i bakgrunden.
Är en högre nivå i applikationsarkitekturen.

Metoder:
Liknar metoderna i IContactRepository, men dessa kan inkludera logik som:
Validering av indata.
Bearbetning av data innan det skickas till eller hämtas från IContactRepository.
Exempel på användning:
Kontrollera att alla obligatoriska fält är ifyllda innan en kontakt skapas.
Lägga till ytterligare logik, t.ex. att skicka e-post när en ny kontakt läggs till.

Detta gränssnitt definierar funktioner för att hantera kontakter i applikationens tjänstelager.

Gränssnittet är till för att:
Hämta alla kontakter eller en specifik kontakt.
Skapa nya kontakter och uppdatera befintliga.
Ta bort kontakter och spara ändringar.
Genom att använda detta gränssnitt kan du hålla logiken för kontakter separerad från datalagring. 
*/


namespace Business.Interfaces
{

    // Detta är ett gränssnitt för att hantera kontaktlogik.
    // Det används för att definiera de funktioner som ska finnas i tjänster som hanterar kontakter.
    public interface IContactService
    {
        // Hämtar alla kontakter som en lista.
        IEnumerable<Contact> GetAllContacts();

        // Hämtar en specifik kontakt baserat på ID.
        Contact GetContactById(Guid id);

        // Skapar en ny kontakt.
        void CreateContact(Contact contact);

        // Uppdaterar informationen för en befintlig kontakt.
        void UpdateContact(Contact contact);

        // Tar bort en kontakt baserat på ID.
        void DeleteContact(Guid id);

        // Sparar alla ändringar som har gjorts.
        void SaveChanges();
    }
}