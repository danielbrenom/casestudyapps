using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Applications;
using Foundation.Abstracts;
using Foundation.Extensions;
using Foundation.Interfaces;
using Xamarin.Forms;

namespace XamarinBaseApp.ViewModels
{
    public class BinaryDecimalVm : INavigableViewModel, INotifyPropertyChanged
    {
        private IApplicationExecutable<Binary2Decimal> BinaryDecimalExecutable { get; }
        private IAlert Alert { get; }
        public ICommand ConvertCommand { get; set; }

        public BinaryDecimalVm(IApplicationExecutable<Binary2Decimal> binaryDecimalExecutable, IAlert alert)
        {
            BinaryDecimalExecutable = binaryDecimalExecutable;
            Alert = alert;
            ConvertCommand = new Command(async () => await Convert());
        }

        public string InputBinary { get; set; }
        public string OutputDecimal { get; set; }

        public void Prepare()
        {
        }

        public void Prepare(IReadOnlyDictionary<string, object> parameters)
        {
        }

        public async Task Initialize()
        {
            await Task.Delay(1);
        }

        private async Task Convert()
        {
            try
            {
                if (InputBinary.IsNullOrEmpty())
                    return;
                if (InputBinary.IndexOfAny("23456789".ToCharArray()) != -1)
                {
                    await Alert.CallAlertAsync("Atention", "Only 0s and 1s must be inputed", new AlertAction("OK"));
                    return;
                }

                BinaryDecimalExecutable.ReceiveParameters(InputBinary);
                var result = await BinaryDecimalExecutable.Execute();
                OutputDecimal = ((int)result).ToString();
            }
            catch (System.Exception e)
            {
                await Alert.CallAlertAsync(e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}