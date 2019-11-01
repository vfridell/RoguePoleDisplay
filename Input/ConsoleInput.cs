using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RoguePoleDisplay.InputListeners;
using RoguePoleDisplay.Models;

namespace RoguePoleDisplay.Input
{
    public class ConsoleInput : IGetInput
    {
        public int GetInteger(int millisecondTimeout)
        {
            throw new NotImplementedException();
        }

        public bool TryGetInteger(out int value, int millisecondTimeout)
        {
            int num = WaitConsoleKey(millisecondTimeout).Result;
            if (num == -1)
            {
                value = 0;
                return false;
            }
            else
            {
                value = num;
                return true;
            }
        }

        public void Init()
        {
        }

        public MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout)
        {
            int num = WaitConsoleKey(millisecondTimeout).Result;
            if (menu.ValidChoice(num))
                return menu.GetMenuItem(num);
            else
                return null;
        }

        private static async Task<int> WaitConsoleKey(int millisecondTimeout)
        {
            try
            {
                ConsoleKey key = default;
                var cancellationTokenSrc = new CancellationTokenSource(millisecondTimeout);
                await Task.Run(() => key = Console.ReadKey(true).Key, cancellationTokenSrc.Token);

                int num = 0;
                switch (key)
                {
                    case ConsoleKey.D0:
                        num = 1;
                        break;
                    case ConsoleKey.D1:
                        num = 1;
                        break;
                    case ConsoleKey.D2:
                        num = 2;
                        break;
                    case ConsoleKey.D3:
                        num = 3;
                        break;
                    case ConsoleKey.D4:
                        num = 4;
                        break;
                    case ConsoleKey.D5:
                        num = 5;
                        break;
                    case ConsoleKey.D6:
                        num = 6;
                        break;
                    case ConsoleKey.D7:
                        num = 7;
                        break;
                    case ConsoleKey.D8:
                        num = 8;
                        break;
                    case ConsoleKey.D9:
                        num = 9;
                        break;
                    default:
                        num = -1;
                        break;

                }
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
