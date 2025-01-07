using Xunit;
using Business.Models;
using Presentation.Console.Services;

/*
    Den här testklassen innehåller enhetstester för ConsoleUserInterface-klassen.
    Testerna verifierar att användargränssnittets grundläggande funktioner fungerar korrekt,
    såsom att visa kontakter, hämta kontaktdetaljer och uppdatera kontakter.
*/
namespace Presentation.Console.Tests
{
    // Denna testklass innehåller alla tester relaterade till ConsoleUserInterface
    // Den testar de olika metoderna som hanterar kontaktinformation
    public class ConsoleUserInterfaceTests
    {
        // _sut (System Under Test) är instansen av klassen vi testar
        private readonly ConsoleUserInterface _sut;

        // Konstruktorn skapar en ny instans av ConsoleUserInterface för varje test
        // Detta säkerställer att varje test börjar med ett rent tillstånd
        public ConsoleUserInterfaceTests()
        {
            _sut = new ConsoleUserInterface();
        }

        // Testar att DisplayContact-metoden inte kastar några undantag
        // Detta är viktigt eftersom metoden hanterar utskrift till konsolen
        [Fact]
        public void DisplayContact_Should_Not_Throw_Exception()
        {
            // Arrange: Skapar en testkontakt med dummy-data
            var contact = new Contact
            {
                FirstName = "Test",
                LastName = "Testsson",
                Email = "test@test.com",
                PhoneNumber = "123456",
                StreetAddress = "Testgatan 1",
                PostalCode = "12345",
                City = "Teststad"
            };

            // Act & Assert: Verifierar att ingen exception kastas när kontakten visas
            // Record.Exception fångar eventuella undantag som kastas under metodanropet
            var exception = Record.Exception(() => _sut.DisplayContact(contact));
            Assert.Null(exception);
        }

        // Testar att GetContactDetails returnerar ett giltigt Contact-objekt
        // Detta verifierar att användarinput hanteras korrekt
        [Fact]
        public void GetContactDetails_Should_Return_Contact_Object()
        {
            // Arrange & Act: Hämtar kontaktdetaljer från användargränssnittet
            var contact = _sut.GetContactDetails();

            // Assert: Verifierar att ett Contact-objekt returneras och inte är null
            Assert.NotNull(contact);
            Assert.IsType<Contact>(contact);
        }

        // Testar att UpdateContactDetails korrekt hanterar uppdatering av en kontakt
        // Detta säkerställer att användaren kan modifiera befintliga kontakter
        [Fact]
        public void UpdateContactDetails_Should_Return_Updated_Contact()
        {
            // Arrange: Skapar en ursprunglig kontakt som ska uppdateras
            var originalContact = new Contact
            {
                FirstName = "Test",
                LastName = "Testsson",
                Email = "test@test.com",
                PhoneNumber = "123456",
                StreetAddress = "Testgatan 1",
                PostalCode = "12345",
                City = "Teststad"
            };

            // Act: Uppdaterar kontakten genom användargränssnittet
            var updatedContact = _sut.UpdateContactDetails(originalContact);

            // Assert: Verifierar att den uppdaterade kontakten är giltig
            Assert.NotNull(updatedContact);
            Assert.IsType<Contact>(updatedContact);
        }
    }
}