using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Foundation.Interfaces;
using Xamarin.Forms;
using XamarinBaseApp.Helpers.VO;

namespace XamarinBaseApp.ViewModels
{
    public class HomePageVm : INavigableViewModel, INotifyPropertyChanged
    {
        private INavigator Navigator { get; }
        public ICommand LoadApplicationCommand { get; }

        public HomePageVm(INavigator navigator)
        {
            Navigator = navigator;
            LoadApplicationCommand = new Command(async () => await LoadApplication());
        }

        public ObservableCollection<DefaultVO> ApplicationsList { get; set; }
        public DefaultVO SelectedApp { get; set; }

        public void Prepare()
        {
            ApplicationsList = new ObservableCollection<DefaultVO>(new[] {new DefaultVO {Name = "Binary to Decimal", Value = "Bin2Dec"}});
        }

        public void Prepare(IReadOnlyDictionary<string, object> parameters)
        {
        }

        public async Task Initialize()
        {
            await Task.Delay(1);
        }

        private async Task LoadApplication()
        {
            if (SelectedApp is null)
                return;
            switch (SelectedApp.Value)
            {
                case "Bin2Dec":
                    await Navigator.NavigateTo<BinaryDecimalVm>();
                    break;
            }

            SelectedApp = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}