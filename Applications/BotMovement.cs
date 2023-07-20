using System;
using System.Threading.Tasks;
using Foundation.Abstracts;

namespace Applications
{
    public class BotMovement : ApplicationExecutable<BotMovement>
    {
        private int[] BotCoordinates { get; set; }
        public override void ReceiveParameters(params object[] parameters)
        {
            BotCoordinates = (int[]) parameters[0];
        }

        public override async Task<object> Execute()
        {
            return await Task.FromResult(BotCanGoToDestination(BotCoordinates[0], BotCoordinates[1],
                                                               BotCoordinates[2], BotCoordinates[3]));
        }

        private bool BotCanGoToDestination(int x1, int y1, int x2, int y2)
        {
            if (x1 > x2 || y1 > y2)
                return false;
            if (x1 + y1 > x2 && x1 + y1 > y2)
                return false;
            if (x1 % 2 == 0 && y1 % 2 == 0 && x2 % 2 == 0 && y2 % 2 == 0)
                return false;
            if (x2 % x1 != 0 || y2 % y1 != 0)
                return false;
            return true;
        }
    }
}