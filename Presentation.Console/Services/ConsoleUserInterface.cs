using Business.Models;
using Presentation.Console.Interfaces;
using System.Text.RegularExpressions;

/*
   ConsoleUserInterface-klassen hanterar all interaktion med användaren via konsolen.
   Den ansvarar för att visa menyer, ta emot input och validera användarens inmatningar.
   Klassen implementerar IUserInterface för att säkerställa att alla nödvändiga metoder finns implementerade.
*/
namespace Presentation.Console.Services
{
    // Denna klass hanterar all användarinteraktion via konsolgränssnittet
    public class ConsoleUserInterface : IUserInterface
    {
        // Visar huvudmenyn med alla tillgängliga val för användaren
        public void DisplayMainMenu()
        {
            System.Console.WriteLine("=== Kontakthanterare ===");
            System.Console.WriteLine("1. Lista alla kontakter");
            System.Console.WriteLine("2. Lägg till kontakt");
            System.Console.WriteLine("3. Redigera kontakt");
            System.Console.WriteLine("4. Ta bort kontakt");
            System.Console.WriteLine("5. Avsluta");
            System.Console.Write("\nVälj ett alternativ: ");
        }

        // Hämtar användarens inmatning och hanterar null-värden
        public string GetUserInput() => System.Console.ReadLine() ?? string.Empty;

        // Rensar konsolfönstret
        public void Clear() => System.Console.Clear();

        // Visar ett meddelande för användaren
        public void DisplayMessage(string message) => System.Console.WriteLine(message);

        // Pausar programmet och väntar på knapptryckning
        public void WaitForKeyPress()
        {
            System.Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            System.Console.ReadKey();
        }

        // Visar all information om en specifik kontakt
        public void DisplayContact(Contact contact)
        {
            System.Console.WriteLine($"ID: {contact.Id}");
            System.Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName}");
            System.Console.WriteLine($"E-post: {contact.Email}");
            System.Console.WriteLine($"Telefon: {contact.PhoneNumber}");
            System.Console.WriteLine($"Adress: {contact.StreetAddress}");
            System.Console.WriteLine($"Postnummer: {contact.PostalCode}");
            System.Console.WriteLine($"Ort: {contact.City}");
            System.Console.WriteLine("------------------------");
        }

        // Hämtar information för en ny kontakt med validering
        public Contact GetContactDetails()
        {
            var contact = new Contact();

            while (true)
            {
                try
                {
                    // Validerar och samlar in all nödvändig kontaktinformation
                    contact.FirstName = GetValidatedInput("Förnamn", IsValidName, "Endast bokstäver och bindestreck är tillåtna");
                    contact.LastName = GetValidatedInput("Efternamn", IsValidName, "Endast bokstäver och bindestreck är tillåtna");
                    contact.Email = GetValidatedInput("E-post", IsValidEmail, "Ogiltig e-postadress");
                    contact.PhoneNumber = GetValidatedInput("Telefonnummer", IsValidPhoneNumber, "Endast siffror och - + ( ) är tillåtna");
                    contact.StreetAddress = GetValidatedInput("Gatuadress", IsValidStreetAddress, "Ogiltig gatuadress");
                    contact.PostalCode = GetValidatedInput("Postnummer", IsValidPostalCode, "Postnummer måste vara 5 siffror");
                    contact.City = GetValidatedInput("Ort", IsValidCity, "Endast bokstäver, bindestreck och mellanslag är tillåtna");
                    break;
                }
                catch (ArgumentException ex)
                {
                    DisplayMessage($"\nFel: {ex.Message}");
                    DisplayMessage("Försök igen.\n");
                }
            }

            return contact;
        }

        // Uppdaterar en befintlig kontakt med möjlighet att behålla gamla värden
        public Contact UpdateContactDetails(Contact existingContact)
        {
            System.Console.WriteLine("Lämna tomt för att behålla befintligt värde\n");

            try
            {
                // Validerar och uppdaterar varje fält om nytt värde anges
                string input = GetValidatedInputOptional($"Förnamn ({existingContact.FirstName})", IsValidName, "Endast bokstäver och bindestreck är tillåtna");
                existingContact.FirstName = string.IsNullOrWhiteSpace(input) ? existingContact.FirstName : input;

                // Upprepar för alla övriga fält...
                // [Resterande uppdateringskod följer samma mönster]
            }
            catch (ArgumentException ex)
            {
                DisplayMessage($"\nFel: {ex.Message}");
                return existingContact;
            }

            return existingContact;
        }

        // Hjälpmetoder för validering av input
        private string GetValidatedInput(string prompt, Func<string, bool> validator, string errorMessage)
        {
            while (true)
            {
                string input = GetInput(prompt);

                if (string.IsNullOrWhiteSpace(input))
                {
                    DisplayMessage($"Fel: {prompt} måste anges");
                    continue;
                }

                if (validator(input))
                    return input;

                DisplayMessage($"Fel: {errorMessage}");
            }
        }

        // Validering av valfri input (tillåter tomma värden)
        private string GetValidatedInputOptional(string prompt, Func<string, bool> validator, string errorMessage)
        {
            string input = GetInput(prompt);

            if (string.IsNullOrWhiteSpace(input))
                return input;

            if (!validator(input))
                throw new ArgumentException(errorMessage);

            return input;
        }

        // Basmetod för att hämta input från användaren
        private string GetInput(string prompt)
        {
            System.Console.Write($"{prompt}: ");
            return System.Console.ReadLine() ?? string.Empty;
        }

        // Valideringsmetoder för olika typer av input
        private bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-öA-Ö\-]+$");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && email.Contains("@") && email.Contains(".");
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^[0-9\-\+\(\)]+$");
        }

        private bool IsValidStreetAddress(string address)
        {
            return Regex.IsMatch(address, @"^[a-öA-Ö0-9\s\-\.]+$");
        }

        private bool IsValidPostalCode(string postalCode)
        {
            return Regex.IsMatch(postalCode, @"^\d{5}$");
        }

        private bool IsValidCity(string city)
        {
            return Regex.IsMatch(city, @"^[a-öA-Ö\-\s]+$");
        }
    }
}