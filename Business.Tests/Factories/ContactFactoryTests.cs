using Business.Factories;
using FluentAssertions;
using Xunit;


/*                                     Vad gör ContactFactoryTests?
Syfte:
Säkerställer att ContactFactory fungerar korrekt genom att skapa och testa kontaktobjekt.

Tester:
CreateContact_ShouldCreateContactWithCorrectProperties:
Kontrollerar att kontakten som skapas har de egenskaper som skickades in.

CreateContact_ShouldCreateUniqueIds_WhenCalledMultipleTimes:
Kontrollerar att varje kontakt som skapas har ett unikt ID. 

 */

namespace Business.Tests.Factories
{
    public class ContactFactoryTests
    {
        private readonly ContactFactory _sut; // sut = System Under Test

        public ContactFactoryTests()
        {
            _sut = new ContactFactory();
        }

        [Fact]
        public void CreateContact_ShouldCreateContactWithCorrectProperties()
        {
            // Arrange
            string firstName = "Ilir";
            string lastName = "vej";
            string email = "Ilir@example.com";
            string phoneNumber = "123456789";
            string streetAddress = "Test Street 1";
            string postalCode = "12345";
            string city = "Test City";

            // Act
            var result = _sut.CreateContact(
                firstName, lastName, email,
                phoneNumber, streetAddress,
                postalCode, city);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBe(Guid.Empty);
            result.FirstName.Should().Be(firstName);
            result.LastName.Should().Be(lastName);
            result.Email.Should().Be(email);
            result.PhoneNumber.Should().Be(phoneNumber);
            result.StreetAddress.Should().Be(streetAddress);
            result.PostalCode.Should().Be(postalCode);
            result.City.Should().Be(city);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateContact_WithInvalidFirstName_ShouldThrowArgumentException(string invalidFirstName)
        {
            // ChatGTP
            // Act
            var action = () => _sut.CreateContact(
                invalidFirstName, "Ilir", "Ilir@example.com",
                "123456789", "Test Street", "12345", "Test City");

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void CreateContact_ShouldCreateUniqueIds()
        {
            // ChatGTP
            // Act
            var contact1 = _sut.CreateContact("Ilir", "Vej", "Ilir@example.com",
                "123", "Street", "12345", "City");
            var contact2 = _sut.CreateContact("Ilirr", "Vejj", "Ilirr@example.com",
                "456", "Street", "12345", "City");

            // Assert
            contact1.Id.Should().NotBe(contact2.Id);
        }
    }
}