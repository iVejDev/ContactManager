using Business.Interfaces;
using Business.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Contact = Business.Models.Contact;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

// Denna ViewModel hanterar:
// 1. En ObservableCollection för att visa kontakter i UI:t.
// 2. Kommandon för att:
//    - Lägga till en ny kontakt (AddContactCommand).
//    - Redigera en existerande kontakt (EditContactCommand).
//    - Uppdatera kontaktlistan (RefreshCommand).
// 3. Navigation via Shell.Current.GoToAsync till olika sidor:
//    - "CreateContactPage" för att skapa nya kontakter.
//    - "EditContactPage" för att redigera existerande kontakter.
// 4. Visar en upptagen-indikator (IsBusy) under laddning av data för att förbättra användarupplevelsen.




namespace Presentation.Maui.ViewModels
{
    public class ContactListViewModel : BaseViewModel
    {
        private readonly IContactService _contactService;
        public ObservableCollection<Contact> Contacts { get; } = new();

        public Command AddContactCommand { get; }
        public Command<Contact> EditContactCommand { get; }
        public Command RefreshCommand { get; }

        public ContactListViewModel(IContactService contactService)
        {
            _contactService = contactService;
            Title = "Kontakter";

            AddContactCommand = new Command(async () => await OnAddContact());
            EditContactCommand = new Command<Contact>(async (contact) => await OnEditContact(contact));
            RefreshCommand = new Command(async () => await LoadContacts());

            // Ladda kontakter när ViewModel skapas
            Task.Run(async () => await LoadContacts());
        }

        private async Task LoadContacts()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Contacts.Clear();

                var contacts = _contactService.GetAllContacts();
                foreach (var contact in contacts)
                {
                    Contacts.Add(contact);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnAddContact()
        {
            await Shell.Current.GoToAsync("CreateContactPage");
        }

        private async Task OnEditContact(Contact contact)
        {
            if (contact == null)
                return;

            var parameters = new Dictionary<string, object>
            {
                { "Contact", contact }
            };

            await Shell.Current.GoToAsync("EditContactPage", parameters);
        }
    }
}