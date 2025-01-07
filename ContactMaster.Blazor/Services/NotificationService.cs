using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ContactMaster.Blazor.Services
{

    // En tjänst som hanterar notifieringar i applikationen.
    public class NotificationService
    {
        // En händelse som utlöses när en notifiering ska visas.
        // Tar emot meddelande och CSS-klass för notifieringsstilen.
        public event Action<string, string>? OnNotification;

        // En ordbok som mappar notifieringstyper till CSS-klasser för styling.
        private readonly Dictionary<string, string > _notificationStyles = new()
        {
            { "success", "notification-success" },
            { "error", "notification-error" },
            { "info", "notification-info" }
        };


        // Visar en notifiering med ett specifikt meddelande och typ.
        // Standardtypen är "info". Notifieringen försvinner efter 3 sekunder.
        public async Task ShowNotification(string message, string type = "info")
        {
            OnNotification?.Invoke(message, _notificationStyles[type]);
            await Task.Delay(3000); // Notification disappears after 3 seconds
        }
        
        // En hjälpmetod för att visa en framgångsnotifiering.
        public async Task ShowSuccess(string message)
            => await ShowNotification(message, "success");
        // En hjälpmetod för att visa ett felmeddelande.
        public async Task ShowError(string message)
            => await ShowNotification(message, "error");

        public async Task ShowWarning(string message)
            => await ShowNotification(message, "warning");
    }
}