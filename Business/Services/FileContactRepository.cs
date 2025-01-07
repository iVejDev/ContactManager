using Business.Interfaces;
using Business.Models;
using System.Text.Json;


/*                                    Vad gör FileContactRepository?
Syfte:
Hanterar lagring och hämtning av kontakter från en JSON-fil.
Håller kontakterna i minnet medan applikationen körs.

Ansvar:
Lagra data i filen när ändringar görs med SaveChanges.
Läsa in data från filen vid start med LoadContacts.
Lägga till, uppdatera, hämta och ta bort kontakter i minnet 

 */


namespace Business.Services
{

    // Denna klass hanterar lagring av kontakter i en fil.
    // Den implementerar IContactRepository för att följa en standard för datalagring.
    public class FileContactRepository : IContactRepository
    {
        private List<Contact> _contacts;
        private readonly string _filePath;


        // Konstruktor som initierar sökvägen till filen och laddar kontakter vid start.
        // Samt updaterar kontakterna när ändringar görs i console app och maui app.
        public FileContactRepository(string filePath = null)
        {
            // CHATGPT hjälp: Path.Combine används för att skapa en sökväg som fungerar på alla plattformar.
            _filePath = filePath ?? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ContactMaster",
                "contacts.json");

            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            _contacts = new List<Contact>();
            LoadContacts();
        }

        // Hämtar alla kontakter som finns i minnet.
        public IEnumerable<Contact> GetAll() => _contacts;

        public Contact GetById(Guid id) => _contacts.FirstOrDefault(c => c.Id == id);

        public void Add(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact));
            _contacts.Add(contact);
        }

        public void Update(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact));
            var index = _contacts.FindIndex(c => c.Id == contact.Id);
            if (index != -1)
            {
                _contacts[index] = contact;
            }
        }

        public void Delete(Guid id)
        {
            var contact = GetById(id);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
        }

        // Sparar alla ändringar i listan till filen.
        public void SaveChanges()
        {
            var json = JsonSerializer.Serialize(_contacts);
            File.WriteAllText(_filePath, json);
            LoadContacts();
        }

        private void LoadContacts()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _contacts = JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
            }
        }
    }
}