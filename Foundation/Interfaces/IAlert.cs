using System.Threading.Tasks;
using Foundation.Abstracts;

namespace Foundation.Interfaces
{
    public interface IAlert
    {
        public Task CallAlertAsync(string title, string message, AlertAction button);
        public Task CallAlertAsync(string title, string message, AlertAction confirmButton, AlertAction cancelButton);
        public Task CallAlertAsync(System.Exception ex, bool popup = false);
    }
}