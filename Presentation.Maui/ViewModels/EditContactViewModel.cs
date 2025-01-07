using Business.Interfaces;
using Business.Models;
using Microsoft.Maui.Controls;
using Contact = Business.Models.Contact;

// Denna ViewModel hanterar:
// 1. Redigering av en befintlig kontakt:
//    - Använder [QueryProperty] för att ta emot kontaktobjektet som ska redigeras.
//    - Har bindningsbara properties för alla kontaktfält som uppdaterar UI i realtid.
// 2. Operationer:
//    - Uppdatering av kontaktinformation via SaveCommand.
//    - Borttagning av en kontakt via DeleteCommand med bekräftelse från användaren.
//    - Avbryt redigering och navigera tillbaka via CancelCommand.
// 3. Felhantering:
//    - Visar felmeddelanden med Shell.Current.DisplayAlert om något går fel.
// 4. Navigation:
//    - Använder Shell.Current.GoToAsync("..") för att återgå till kontaktlistan efter alla operationer.



namespace Presentation.Maui.ViewModels
{
    [QueryProperty(nameof(Contact), "Contact")]
    public class EditContactViewModel : BaseViewModel
    {
        private readonly IContactService _contactService;
        private Contact _contact;

        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _streetAddress;
        private string _postalCode;
        private string _city;

        public Contact Contact
        {
            get => _contact;
            set
            {
                _contact = value;
                LoadContact();
            }
        }

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
        public Command DeleteCommand { get; }
        public Command CancelCommand { get; }

        public EditContactViewModel(IContactService contactService)
        {
            _contactService = contactService;
            Title = "Redigera Kontakt";

            SaveCommand = new Command(async () => await OnSave());
            DeleteCommand = new Command(async () => await OnDelete());
            CancelCommand = new Command(async () => await OnCancel());
        }

        private void LoadContact()
        {
            FirstName = Contact.FirstName;
            LastName = Contact.LastName;
            Email = Contact.Email;
            PhoneNumber = Contact.PhoneNumber;
            StreetAddress = Contact.StreetAddress;
            PostalCode = Contact.PostalCode;
            City = Contact.City;
        }

        private async Task OnSave()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                Contact.FirstName = FirstName;
                Contact.LastName = LastName;
                Contact.Email = Email;
                Contact.PhoneNumber = PhoneNumber;
                Contact.StreetAddress = StreetAddress;
                Contact.PostalCode = PostalCode;
                Contact.City = City;

                _contactService.UpdateContact(Contact);
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

        private async Task OnDelete()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                bool answer = await Shell.Current.DisplayAlert(
                    "Ta bort kontakt",
                    $"Är du säker på att du vill ta bort {FirstName} {LastName}?",
                    "Ja", "Nej");

                if (answer)
                {
                    _contactService.DeleteContact(Contact.Id);
                    await Shell.Current.GoToAsync("..");
                }
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