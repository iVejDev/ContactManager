using Business.Interfaces;
using Business.Models;
using Business.Services;
using FluentAssertions;
using Moq;
using Xunit;


/*                                     Vad gör ContactServiceTests?
Syfte:
Säkerställer att ContactService fungerar korrekt och att alla metoder anropar repository på rätt sätt.

Tester:
GetAllContacts_ShouldReturnAllContacts:
Testar att GetAllContacts hämtar alla kontakter från repository.

CreateContact_ShouldValidateAndSaveContact:
Testar att CreateContact validerar och sparar en ny kontakt.

CreateContact_ShouldThrowException_WhenFirstNameIsInvalid:
Testar att ett undantag kastas om förnamnet är ogiltigt (null eller tomt).

DeleteContact_ShouldDeleteAndSaveChanges:
Testar att DeleteContact raderar en kontakt och sparar ändringarna.

Varför använda Moq?
Säkerställer rätt beteende:
Moq används för att skapa "mock"-objekt, som låter oss simulera och verifiera anrop till IContactRepository utan att behöva en riktig implementation.

Snabbare tester:
Inga faktiska databaser eller filer används, vilket gör testerna snabbare. 

 */




namespace Business.Tests.Services
{
    public class ContactServiceTests
    {
        private readonly ContactService _sut;
        private readonly Mock<IContactRepository> _repositoryMock;

        public ContactServiceTests()
        {
            _repositoryMock = new Mock<IContactRepository>();
            _sut = new ContactService(_repositoryMock.Object);
        }

        [Fact]
        public void GetAllContacts_ShouldReturnAllContacts()
        {
            // Arrange
            var expectedContacts = new List<Contact>
            {
                new Contact { FirstName = "Test1", LastName = "Person1", Email = "test1@test.com" },
                new Contact { FirstName = "Test2", LastName = "Person2", Email = "test2@test.com" }
            };
            _repositoryMock.Setup(x => x.GetAll()).Returns(expectedContacts);

            // Act
            var result = _sut.GetAllContacts();

            // Assert
            result.Should().BeEquivalentTo(expectedContacts);
            _repositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void CreateContact_WithValidData_ShouldSaveContact()
        {
            // Arrange
            var contact = new Contact
            {
                FirstName = "Test",
                LastName = "Person",
                Email = "test@test.com",
                PhoneNumber = "12345",
                StreetAddress = "Street",
                PostalCode = "12345",
                City = "City"
            };

            // Act
            _sut.CreateContact(contact);

            // Assert
            _repositoryMock.Verify(x => x.Add(contact), Times.Once);
            _repositoryMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void CreateContact_WithInvalidEmail_ShouldThrowException()
        {
            // Arrange
            var contact = new Contact
            {
                FirstName = "Test",
                LastName = "Person",
                Email = "invalid-email",
                PhoneNumber = "12345",
                StreetAddress = "Street",
                PostalCode = "12345",
                City = "City"
            };

            // Act
            var action = () => _sut.CreateContact(contact);

            // Assert
            action.Should().Throw<ArgumentException>()
                  .WithMessage("Ogiltig e-postadress");
        }

        [Fact]
        public void UpdateContact_WithValidData_ShouldUpdateAndSave()
        {
            // Arrange
            var contact = new Contact
            {
                FirstName = "Test",
                LastName = "Person",
                Email = "test@test.com",
                PhoneNumber = "12345",
                StreetAddress = "Street",
                PostalCode = "12345",
                City = "City"
            };

            // Act
            _sut.UpdateContact(contact);

            // Assert
            _repositoryMock.Verify(x => x.Update(contact), Times.Once);
            _repositoryMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteContact_ShouldDeleteAndSave()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            _sut.DeleteContact(id);

            // Assert
            _repositoryMock.Verify(x => x.Delete(id), Times.Once);
            _repositoryMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Theory]
        [InlineData("123name")] // Siffror i namn
        [InlineData("Name123")] // Siffror i namn
        [InlineData("Na@me")] // Specialtecken i namn
        public void CreateContact_WithInvalidName_ShouldThrowException(string invalidName)
        {
            // Arrange
            var contact = new Contact
            {
                FirstName = invalidName,
                LastName = "Valid",
                Email = "test@test.com",
                PhoneNumber = "12345",
                StreetAddress = "Street",
                PostalCode = "12345",
                City = "City"
            };

            // Act
            var action = () => _sut.CreateContact(contact);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}