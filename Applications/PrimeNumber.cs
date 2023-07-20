using System.Threading.Tasks;
using Foundation.Abstracts;

namespace Applications
{
    public class PrimeNumber : ApplicationExecutable<PrimeNumber>
    {
        private int Number { get; set; }
        private int Iterations { get; set; }
        public override void ReceiveParameters(params object[] parameters)
        {
            Number = (int)parameters[0];
        }

        public override async Task<object> Execute()
        {
            var isPrime = CheckPrime();
            return await Task.FromResult($"O número {Number} {(isPrime ? "é" : "não é")} primo. Número de iterações necessárias: {Iterations}");
        }

        private bool CheckPrime()
        {
            Iterations = 1;
            if (Number == 2 || Number == 3)
                return true;
            if (Number == 1 || Number % 2 == 0)
                return false;
            for (var i = 5; i <= Number/2; i += 2)
            {
                if (Number % i == 0)
                {
                    return false;
                }
                Iterations++;
            }

            return true;
        }
    }
}