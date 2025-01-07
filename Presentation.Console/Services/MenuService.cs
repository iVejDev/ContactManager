using Business.Interfaces;
using Presentation.Console.Interfaces;


/*
                                    Vad gör MenuService?

Syfte:
Hanterar menylogiken för konsolapplikationen.
Tar emot inmatning från användaren och utför rätt åtgärd baserat på deras val.

Ansvar:
Visa menyer och ta emot användarens val.
Anropa lämpliga tjänster för att hantera kontakter.
Säkerställa att användaren får rätt meddelanden och felhantering.

Varför använda MenuService?
Enkelt att läsa och underhålla:
Separerar menylogik från andra delar av applikationen.

Flexibilitet:
Om menyval eller logik ändras, påverkar det inte andra komponenter
*/


namespace Presentation.Console.Services
{


    // Hanterar menylogiken i konsolapplikationen.
    // Använder IUserInterface för att kommunicera med användaren och IContactService för att hantera kontakter.
    public class MenuService : IMenuService
    {

        // Tjänst för att hantera kontaktlogik.
        // Gränssnitt för att interagera med användaren.
        // Fabrik för att skapa nya kontakter.
        private readonly IContactService _contactService;
        private readonly IUserInterface _ui;
        private readonly IContactFactory _contactFactory;
        private IContactService object1;
        private IUserInterface object2;

        // Konstruktor som initierar tjänster och gränssnitt som används i menyn.
        public MenuService(IContactService contactService, IUserInterface ui, IContactFactory contactFactory)
        {
            _contactService = contactService;
            _ui = ui;
            _contactFactory = contactFactory;
        }

        public MenuService(IContactService object1, IUserInterface object2)
        {
            this.object1 = object1;
            this.object2 = object2;
        }

        // Kör huvudmenyn tills användaren väljer att avsluta.
        public void Run()
        {
            bool running = true;
            while (running)
            {
                _ui.Clear();
                _ui.DisplayMainMenu();

                switch (_ui.GetUserInput())
                {
                    case "1":
                        ListAllContacts();
                        break;
                    case "2":
                        AddNewContact();
                        break;
                    case "3":
                        EditContact();
                        break;
                    case "4":
                        DeleteContact();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        _ui.DisplayMessage("Ogiltigt val!");
                        _ui.WaitForKeyPress();
                        break;
                }
            }
        }


        // Visar alla sparade kontakter.
        private void ListAllContacts()
        {
            _ui.Clear();
            _ui.DisplayMessage("=== Alla Kontakter ===\n");

            var contacts = _contactService.GetAllContacts();
            if (!contacts.Any())
            {
                _ui.DisplayMessage("Inga kontakter finns sparade.");
            }
            else
            {
                foreach (var contact in contacts)
                {
                    _ui.DisplayContact(contact);
                }
            }
            _ui.WaitForKeyPress();
        }

        private void AddNewContact()
        {
            _ui.Clear();
            _ui.DisplayMessage("=== Lägg till ny kontakt ===\n");

            try
            {
                var contact = _ui.GetContactDetails();
                _contactService.CreateContact(contact);
                _ui.DisplayMessage("\nKontakten har sparats!");
            }
            catch (Exception ex)
            {
                _ui.DisplayMessage($"\nFel: {ex.Message}");
            }

            _ui.WaitForKeyPress();
        }

        private void EditContact()
        {
            _ui.Clear();
            _ui.DisplayMessage("=== Redigera kontakt ===\n");
            _ui.DisplayMessage("Ange ID på kontakten du vill redigera: ");

            if (Guid.TryParse(_ui.GetUserInput(), out Guid id))
            {
                var contact = _contactService.GetContactById(id);
                if (contact != null)
                {
                    try
                    {
                        var updatedContact = _ui.UpdateContactDetails(contact);
                        _contactService.UpdateContact(updatedContact);
                        _ui.DisplayMessage("\nKontakten har uppdaterats!");
                    }
                    catch (Exception ex)
                    {
                        _ui.DisplayMessage($"\nFel: {ex.Message}");
                    }
                }
                else
                {
                    _ui.DisplayMessage("Ingen kontakt hittades med det ID:t.");
                }
            }
            else
            {
                _ui.DisplayMessage("Ogiltigt ID format.");
            }

            _ui.WaitForKeyPress();
        }

        private void DeleteContact()
        {
            _ui.Clear();
            _ui.DisplayMessage("=== Ta bort kontakt ===\n");
            _ui.DisplayMessage("Ange ID på kontakten du vill ta bort: ");

            if (Guid.TryParse(_ui.GetUserInput(), out Guid id))
            {
                var contact = _contactService.GetContactById(id);
                if (contact != null)
                {
                    _ui.DisplayMessage($"\nÄr du säker på att du vill ta bort kontakten: {contact.FirstName} {contact.LastName}? (j/n)");
                    if (_ui.GetUserInput().ToLower() == "j")
                    {
                        _contactService.DeleteContact(id);
                        _ui.DisplayMessage("Kontakten har tagits bort!");
                    }
                    else
                    {
                        _ui.DisplayMessage("Borttagning avbruten.");
                    }
                }
                else
                {
                    _ui.DisplayMessage("Ingen kontakt hittades med det ID:t.");
                }
            }
            else
            {
                _ui.DisplayMessage("Ogiltigt ID format.");
            }

            _ui.WaitForKeyPress();
        }
    }
}