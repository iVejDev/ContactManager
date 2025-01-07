using Business.Models;

/* 
                                    IContactRepository
Ansvar:
Hanterar direkt interaktion med datakällan (t.ex. en databas eller en fil).
Fokuserar på CRUD-operationer (Create, Read, Update, Delete) på en grundläggande nivå.
Är en låg nivå i applikationsarkitekturen.

Metoder:
GetAll, GetById, Add, Update, Delete, SaveChanges.
Dessa metoder hanterar endast data, utan extra logik.
Exempel på användning:
Läsa in alla kontakter från en JSON-fil.
Skriva nya eller uppdaterade kontakter till en fil.

Detta är ett gränssnitt som fungerar som en mall för klasser som hanterar datalagring.

Gränssnittet definierar metoder för:
Att hämta alla kontakter (GetAll).
Hämta en specifik kontakt med ID (GetById).
Lägga till, uppdatera och ta bort kontakter.
Spara ändringar med SaveChanges. 
*/

namespace Business.Interfaces
{

    // Detta är ett gränssnitt för att hantera kontakter i en datakälla (t.ex. en fil eller databas).
    // Det innehåller de metoder som krävs för att läsa, skapa, uppdatera och ta bort kontakter.
    public interface IContactRepository
    {
        // Hämtar alla kontakter som en lista.
        IEnumerable<Contact> GetAll();
        
        // Hämtar en specifik kontakt baserat på ID.
        Contact GetById(Guid id);
        
        // Lägger till en ny kontakt.
        void Add(Contact contact);
        
        // Uppdaterar en befintlig kontakt.
        void Update(Contact contact);
        
        // Tar bort en kontakt baserat på ID.
        void Delete(Guid id);
        
        // Sparar alla ändringar till datakällan.
        void SaveChanges();
    }
}