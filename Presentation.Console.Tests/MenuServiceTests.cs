using Xunit;
using Moq;
using Business.Interfaces;
using Business.Models;
using Presentation.Console.Interfaces;
using Presentation.Console.Services;
using System.Collections.Generic;

/*
   Denna testklass ansvarar för att testa MenuService-klassen.
   MenuService hanterar programmets huvudmeny och dess olika funktioner.
   Testerna verifierar att menyn visas korrekt och att alla menyval fungerar som förväntat.
*/
namespace Presentation.Console.Tests
{
    // MenuServiceTests innehåller enhetstester för MenuService
    // Den använder Moq för att mocka beroenden och isolera testerna
    public class MenuServiceTests
    {
        // Mockobjekt för att simulera ContactService och UserInterface
        private readonly Mock<IContactService> _contactServiceMock;
        private readonly Mock<IUserInterface> _userInterfaceMock;

        // System Under Test - instansen av MenuService som testas
        private readonly MenuService _sut;

        // Konstruktorn sätter upp alla nödvändiga mockobjekt och initierar MenuService
        // Detta körs före varje testmetod för att säkerställa ett rent utgångsläge
        public MenuServiceTests()
        {
            _contactServiceMock = new Mock<IContactService>();
            _userInterfaceMock = new Mock<IUserInterface>();
            _sut = new MenuService(_contactServiceMock.Object, _userInterfaceMock.Object);
        }

        // Testar att huvudmenyn visas när programmet körs
        [Fact]
        public void Run_Should_Display_MainMenu()
        {
            // Arrange: Ställer in mock för att simulera att användaren väljer att avsluta
            _userInterfaceMock.Setup(ui => ui.GetUserInput()).Returns("5"); // Exit option

            // Act: Kör huvudprogramloopen
            _sut.Run();

            // Assert: Verifierar att huvudmenyn visades en gång
            _userInterfaceMock.Verify(ui => ui.DisplayMainMenu(), Times.Once);
        }

        // Testar att alla kontakter visas korrekt när användaren väljer listningsfunktionen
        [Fact]
        public void ListContacts_Should_Display_All_Contacts()
        {
            // Arrange: Skapar testdata och ställer in mocks
            var contacts = new List<Contact>
           {
               new Contact { FirstName = "Test", LastName = "Person" }
           };
            _contactServiceMock.Setup(cs => cs.GetAllContacts()).Returns(contacts);
            // Simulerar att användaren först väljer att lista kontakter (1) och sedan avslutar (5)
            _userInterfaceMock.Setup(ui => ui.GetUserInput()).Returns("1").Callback(() =>
                _userInterfaceMock.Setup(ui => ui.GetUserInput()).Returns("5"));

            // Act: Kör huvudprogramloopen
            _sut.Run();

            // Assert: Verifierar att kontakterna hämtades och visades
            _contactServiceMock.Verify(cs => cs.GetAllContacts(), Times.Once);
            _userInterfaceMock.Verify(ui => ui.DisplayContact(It.IsAny<Contact>()), Times.Once);
        }

        // Testar att en ny kontakt skapas korrekt när användaren väljer att lägga till
        [Fact]
        public void AddContact_Should_Create_New_Contact()
        {
            // Arrange: Skapar testdata och ställer in mocks
            var newContact = new Contact { FirstName = "Test", LastName = "Person" };
            // Simulerar att användaren först väljer att lägga till kontakt (2) och sedan avslutar (5)
            _userInterfaceMock.Setup(ui => ui.GetUserInput()).Returns("2").Callback(() =>
                _userInterfaceMock.Setup(ui => ui.GetUserInput()).Returns("5"));
            _userInterfaceMock.Setup(ui => ui.GetContactDetails()).Returns(newContact);

            // Act: Kör huvudprogramloopen
            _sut.Run();

            // Assert: Verifierar att en ny kontakt skapades
            _contactServiceMock.Verify(cs => cs.CreateContact(It.IsAny<Contact>()), Times.Once);
        }

        // Testar att en kontakt kan tas bort korrekt när användaren väljer borttagningsfunktionen
        [Fact]
        public void DeleteContact_Should_Remove_Contact()
        {
            // Arrange: Skapar testdata och ställer in mocks
            var contactId = Guid.NewGuid();
            var contact = new Contact { Id = contactId, FirstName = "Test", LastName = "Person" };
            // Simulerar att användaren först väljer att ta bort kontakt (4) och sedan avslutar (5)
            _userInterfaceMock.Setup(ui => ui.GetUserInput()).Returns("4").Callback(() =>
                _userInterfaceMock.Setup(ui => ui.GetUserInput()).Returns("5"));
            _contactServiceMock.Setup(cs => cs.GetContactById(contactId)).Returns(contact);

            // Act: Kör huvudprogramloopen
            _sut.Run();

            // Assert: Verifierar att kontakten togs bort
            _contactServiceMock.Verify(cs => cs.DeleteContact(It.IsAny<Guid>()), Times.Once);
        }
    }
}