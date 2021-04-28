using System;
using System.Threading.Tasks;
using Foundation.Abstracts;
using Foundation.Interfaces;
using Xamarin.Forms;

namespace XamarinBaseApp.Services
{
    public class Alert : IAlert
    {
        private static NavigationPage CurrentPage => Application.Current.MainPage as NavigationPage;

        public async Task CallAlertAsync(string title, string message, AlertAction button)
        {
            await CurrentPage.DisplayAlert(title, message, button?.Text ?? "OK");
            button?.ActionButton?.Invoke();
        }

        public async Task CallAlertAsync(string title, string message, AlertAction confirmButton,
            AlertAction cancelButton)
        {
            var choice = await CurrentPage.DisplayAlert(title, message,
                                                        confirmButton?.Text ?? "OK", cancelButton?.Text ?? "Cancel");
            if (choice)
                confirmButton?.ActionButton?.Invoke();
            else
                cancelButton?.ActionButton?.Invoke();
        }

        public async Task CallAlertAsync(Exception ex, bool popup = false)
        {
            await CurrentPage.DisplayAlert("Error", "An error occured", "OK");
        }
    }
}