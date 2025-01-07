using Business.Interfaces;
using Business.Models;
using System.Text.RegularExpressions;



/*
 * Den här klassen, ContactService, hanterar affärslogiken för kontaktobjekt.
 * Den tillhandahåller metoder för att hämta, skapa, uppdatera, ta bort och validera kontakter,
 * samt ser till att data sparas genom att använda ett kontaktrepository.
 * Den innehåller också flera privata metoder för att validera kontaktens egenskaper
 * som namn, e-postadress, telefonnummer, gatuadress, postnummer och ort.
 */


namespace Business.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _repository.GetAll();
        }

        public Contact GetContactById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void CreateContact(Contact contact)
        {
            ValidateContact(contact);
            _repository.Add(contact);
            _repository.SaveChanges();
        }

        public void UpdateContact(Contact contact)
        {
            ValidateContact(contact);
            _repository.Update(contact);
            _repository.SaveChanges();
        }

        public void DeleteContact(Guid id)
        {
            _repository.Delete(id);
            _repository.SaveChanges();
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        private void ValidateContact(Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            // Validera förnamn
            if (string.IsNullOrWhiteSpace(contact.FirstName))
                throw new ArgumentException("Förnamn måste anges");
            if (!IsValidName(contact.FirstName))
                throw new ArgumentException("Förnamn får endast innehålla bokstäver och bindestreck");

            // Validera efternamn
            if (string.IsNullOrWhiteSpace(contact.LastName))
                throw new ArgumentException("Efternamn måste anges");
            if (!IsValidName(contact.LastName))
                throw new ArgumentException("Efternamn får endast innehålla bokstäver och bindestreck");

            // Validera e-post
            if (string.IsNullOrWhiteSpace(contact.Email))
                throw new ArgumentException("E-post måste anges");
            if (!IsValidEmail(contact.Email))
                throw new ArgumentException("Ogiltig e-postadress");

            // Validera telefonnummer
            if (string.IsNullOrWhiteSpace(contact.PhoneNumber))
                throw new ArgumentException("Telefonnummer måste anges");
            if (!IsValidPhoneNumber(contact.PhoneNumber))
                throw new ArgumentException("Telefonnummer får endast innehålla siffror och - + ( )");

            // Validera gatuadress
            if (string.IsNullOrWhiteSpace(contact.StreetAddress))
                throw new ArgumentException("Gatuadress måste anges");
            if (!IsValidStreetAddress(contact.StreetAddress))
                throw new ArgumentException("Ogiltig gatuadress");

            // Validera postnummer
            if (string.IsNullOrWhiteSpace(contact.PostalCode))
                throw new ArgumentException("Postnummer måste anges");
            if (!IsValidPostalCode(contact.PostalCode))
                throw new ArgumentException("Postnummer måste vara 5 siffror");

            // Validera ort
            if (string.IsNullOrWhiteSpace(contact.City))
                throw new ArgumentException("Ort måste anges");
            if (!IsValidCity(contact.City))
                throw new ArgumentException("Ort får endast innehålla bokstäver och bindestreck");
        }

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