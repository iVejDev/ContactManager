using Business.Interfaces;
using Business.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;


// Denna ViewModel hanterar:
// 1. Fält och bindning för att skapa en ny kontakt, inklusive:
//    - Förnamn, efternamn, e-post, telefonnummer, adress, postnummer och stad.
// 2. PropertyChanged-mekanismen för att uppdatera UI när användaren ändrar data.
// 3. Kommandon för:
//    - Spara (SaveCommand): Skapar en ny kontakt med hjälp av ContactFactory och navigerar tillbaka till kontaktlistan.
//    - Avbryt (CancelCommand): Navigerar tillbaka till kontaktlistan utan att spara.
// 4. ContactFactory för att skapa nya kontakter från användarinmatning.
// 5. Felhantering: Visar ett felmeddelande med Shell.Current.DisplayAlert om något går fel vid sparning.
// 6. Navigation: Använder Shell.Current.GoToAsync för att navigera tillbaka till föregående sida efter spara eller avbryt.



namespace Presentation.Maui.ViewModels
{
    public class CreateContactViewModel : BaseViewModel
    {
        private readonly IContactService _contactService;
        private readonly IContactFactory _contactFactory;

        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _streetAddress;
        private string _postalCode;
        private string _city;

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string StreetAddress
        {
            get => _streetAddress;
            set => SetProperty(ref _streetAddress, value);
        }

        public string PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }

        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public CreateContactViewModel(IContactService contactService, IContactFactory contactFactory)
        {
            _contactService = contactService;
            _contactFactory = contactFactory;

            Title = "Ny Kontakt";

            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await OnCancel());
        }

        private async Task OnSave()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var contact = _contactFactory.CreateContact(
                    FirstName, LastName, Email,
                    PhoneNumber, StreetAddress,
                    PostalCode, City);

                _contactService.CreateContact(contact);

                // Gå tillbaka till kontaktlistan
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Fel", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}