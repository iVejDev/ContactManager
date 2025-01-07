using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;



/*
 * Den här klassen representerar en kontaktmodell med flera egenskaper 
 * som förnamn, efternamn, e-postadress, telefonnummer, gatuadress, postnummer och ort.
 * Varje egenskap har valideringsattribut för att säkerställa att inmatad data är korrekt formaterad.
 * Klassen implementerar också IValidatableObject för att möjliggöra anpassad valideringslogik vid behov.
 */

namespace Business.Models
{
    public class Contact : IValidatableObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Förnamn måste anges")]
        [RegularExpression(@"^[a-öA-Ö\-]+$", ErrorMessage = "Förnamn får endast innehålla bokstäver och bindestreck")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn måste anges")]
        [RegularExpression(@"^[a-öA-Ö\-]+$", ErrorMessage = "Efternamn får endast innehålla bokstäver och bindestreck")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-post måste anges")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefonnummer måste anges")]
        [RegularExpression(@"^[0-9\-\+\(\)]+$", ErrorMessage = "Telefonnummer får endast innehålla siffror och - + ( )")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gatuadress måste anges")]
        [RegularExpression(@"^[a-öA-Ö0-9\s\-\.]+$", ErrorMessage = "Ogiltig gatuadress")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Postnummer måste anges")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Postnummer måste vara 5 siffror")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Ort måste anges")]
        [RegularExpression(@"^[a-öA-Ö\-\s]+$", ErrorMessage = "Ort får endast innehålla bokstäver och bindestreck")]
        public string City { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}