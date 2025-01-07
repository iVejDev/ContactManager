using Business.Models;

/*                      Vad gör koden?

Detta är ett gränssnitt som fungerar som en mall för klasser.
Alla klasser som implementerar IContactFactory måste ha en metod som heter CreateContact.
Syftet är att säkerställa att alla som använder detta gränssnitt vet vilka funktioner som finns tillgängliga. 

 */

namespace Business.Interfaces
{

    // Detta är ett gränssnitt för att skapa kontakter.
    // Det definierar vad en kontaktfabrik (ContactFactory) måste kunna göra.

    public interface IContactFactory
    {

     // Metoden som alla klasser som implementerar detta gränssnitt måste ha.
     // Den ska skapa en ny kontakt med den information som skickas in.


        Contact CreateContact(string firstName, string lastName, string email,
            string phoneNumber, string streetAddress, string postalCode, string city);
    }
}