using System.Threading.Tasks;
using Foundation.Abstracts;

namespace Applications
{
    public class Binary2Decimal : ApplicationExecutable<Binary2Decimal>
    {
        private string BinarySequence { get; set; }

        public override void ReceiveParameters(params object[] parameters)
        {
            BinarySequence = (string) parameters[0];
        }

        public override async Task<object> Execute()
        {
            return await Task.Run(() =>
            {
                var decimalValue = 0;
                foreach (var binaryValue in BinarySequence)
                {
                    decimalValue = decimalValue * 2 + (binaryValue - 48);
                }

                return decimalValue;
            });
        }
    }
}