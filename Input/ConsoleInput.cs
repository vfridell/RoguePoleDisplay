using System;
using System.Collections.Generic;
using System.IO;
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
        private EventWaitHandle _waitHandle = new AutoResetEvent(false);
        private InputData _lastObservedInputData = new InputData();
        private Task _inputTask;

        public int GetInteger(int millisecondTimeout)
        {
            throw new NotImplementedException();
        }

        public bool TryGetInteger(out int value, int millisecondTimeout)
        {
            _lastObservedInputData.LastNumEntered = -1;
            if(_inputTask == null || _inputTask.IsCompleted || _inputTask.IsFaulted) _inputTask = new Task(WaitConsoleKey);

            if(!(_inputTask.Status == TaskStatus.Running)) _inputTask.Start();
            Task.WaitAny(_inputTask, Task.Delay(millisecondTimeout));

            if (_lastObservedInputData.LastNumEntered == -1)
            {
                value = 0;
                return false;
            }
            else
            {
                value = _lastObservedInputData.LastNumEntered;
                return true;
            }
        }

        public void Init()
        {
        }

        public MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout)
        {

            _lastObservedInputData.LastNumEntered = -1;
            if(_inputTask == null || _inputTask.IsCompleted || _inputTask.IsFaulted) _inputTask = new Task(WaitConsoleKey);

            if(!(_inputTask.Status == TaskStatus.Running)) _inputTask.Start();
            Task.WaitAny(_inputTask, Task.Delay(millisecondTimeout));

            int num = _lastObservedInputData.LastNumEntered;
            if (menu.ValidChoice(num))
                return menu.GetMenuItem(num);
            else
                return null;
        }

        private void WaitConsoleKey()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            int num = 0;
            switch (keyInfo.Key)
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
            _lastObservedInputData.LastNumEntered = num;
        }
    }

}
