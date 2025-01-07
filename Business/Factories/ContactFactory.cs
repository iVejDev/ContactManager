using Business.Models;
using Business.Interfaces;

/*                                  Den här koden används för att skapa kontakter.

CreateContact tar emot den information som behövs (t.ex. förnamn och e-post) och skapar ett kontaktobjekt.
Detta gör det enkelt att skapa nya kontakter utan att upprepa samma kod på flera ställen. 

 */



namespace Business.Factories
{


    // Denna klass skapar nya kontakter.
    // Den används för att samla logiken för att skapa kontakter på ett ställe.

    public class ContactFactory : IContactFactory
    {


        // Den här metoden skapar en ny kontakt.
        // Den tar emot information som förnamn, efternamn och adress
        // och returnerar ett färdigt kontaktobjekt.

        public Contact CreateContact(string firstName, string lastName, string email,
            string phoneNumber, string streetAddress, string postalCode, string city)
        {

            // Här skapas en ny kontakt med den information som skickas in.
            // Id:t genereras automatiskt när en ny kontakt skapas.

            return new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city
            };
        }
    }
}