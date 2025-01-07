using Business.Models;
using Business.Services;
using FluentAssertions;
using System.Text.Json;
using Xunit;


/*                                     Vad gör FileContactRepositoryTests?
Syfte:
Säkerställer att FileContactRepository hanterar kontakter korrekt, inklusive lagring och uppdatering i filen.

Tester:
AddContact_ShouldAddContactToList:
Kontrollerar att en kontakt läggs till i listan.

GetById_ShouldReturnCorrectContact:
Säkerställer att rätt kontakt returneras baserat på ID.

SaveChanges_ShouldPersistDataToFile:
Kontrollerar att kontakterna sparas korrekt till filen.

Update_ShouldUpdateExistingContact:
Testar att en kontakt kan uppdateras.

Delete_ShouldRemoveContact:
Verifierar att en kontakt tas bort korrekt. 

 */



namespace Business.Tests.Repositories
{
    public class FileContactRepositoryTests : IDisposable
    {
        private readonly string _testFilePath;
        private readonly FileContactRepository _sut;

        public FileContactRepositoryTests()
        {
            // Skapa en unik test-fil för varje test
            _testFilePath = Path.Combine(Path.GetTempPath(), $"contacts_test_{Guid.NewGuid()}.json");
            _sut = new FileContactRepository(_testFilePath);
        }

        [Fact]
        public void Add_ShouldAddContactToList()
        {
            // Arrange
            var contact = CreateTestContact();

            // Act
            _sut.Add(contact);
            var result = _sut.GetAll();

            // Assert
            result.Should().ContainSingle();
            result.First().Should().BeEquivalentTo(contact);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectContact()
        {
            // Arrange
            var contact = CreateTestContact();
            _sut.Add(contact);

            // Act
            var result = _sut.GetById(contact.Id);

            // Assert
            result.Should().BeEquivalentTo(contact);
        }

        [Fact]
        public void GetAll_WithNoContacts_ShouldReturnEmptyList()
        {
            // Act
            var result = _sut.GetAll();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Update_ShouldUpdateExistingContact()
        {
            // Arrange
            var contact = CreateTestContact();
            _sut.Add(contact);

            // Act
            contact.FirstName = "UpdatedName";
            _sut.Update(contact);

            // Assert
            var updated = _sut.GetById(contact.Id);
            updated.FirstName.Should().Be("UpdatedName");
        }

        [Fact]
        public void Delete_ShouldRemoveContact()
        {
            // Arrange
            var contact = CreateTestContact();
            _sut.Add(contact);

            // Act
            _sut.Delete(contact.Id);

            // Assert
            var result = _sut.GetAll();
            result.Should().BeEmpty();
        }

        [Fact]
        public void SaveChanges_ShouldPersistToFile()
        {
            // Arrange
            var contact = CreateTestContact();
            _sut.Add(contact);

            // Act
            _sut.SaveChanges();

            // Assert
            File.Exists(_testFilePath).Should().BeTrue();
            var savedJson = File.ReadAllText(_testFilePath);
            var savedContacts = JsonSerializer.Deserialize<List<Contact>>(savedJson);
            savedContacts.Should().ContainEquivalentOf(contact);
        }

        [Fact]
        public void Constructor_WithExistingFile_ShouldLoadContacts()
        {
            // Arrange
            var contact = CreateTestContact();
            var contacts = new List<Contact> { contact };
            var json = JsonSerializer.Serialize(contacts);
            File.WriteAllText(_testFilePath, json);

            // Act
            var repository = new FileContactRepository(_testFilePath);
            var result = repository.GetAll();

            // Assert
            result.Should().ContainEquivalentOf(contact);
        }

        private Contact CreateTestContact()
        {
            return new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Person",
                Email = "test@example.com",
                PhoneNumber = "123-456",
                StreetAddress = "Test Street 1",
                PostalCode = "12345",
                City = "Test City"
            };
        }

        public void Dispose()
        {
            // Cleanup: Ta bort testfilen efter varje test
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }
    }
}