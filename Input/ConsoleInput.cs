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
        Interaction _interaction;
        bool _interactionHappened = false;
        AutoResetEvent getInput = new AutoResetEvent(false);

        public int GetInteger(int millisecondTimeout)
        {
            using (var consoleListener = new ConsoleListener())
            {
                _interaction = new Interaction();
                consoleListener.InteractionHandled += consoleListener_InteractionHandled;
                consoleListener.Start();
                getInput.WaitOne(millisecondTimeout);
                return _interaction.resultValue;
            }
        }

        void consoleListener_InteractionHandled(object sender, InteractionEventArgs e)
        {
            lock (_interaction)

            {
                _interaction = e.interaction;
                _interactionHappened = true;
                getInput.Set();
            }
        }

        public bool TryGetInteger(out int value, int millisecondTimeout)
        {
            using (var consoleListener = new ConsoleListener())
            {
                _interactionHappened = false;
                _interaction = new Interaction();
                consoleListener.InteractionHandled += consoleListener_InteractionHandled;
                consoleListener.Start();
                getInput.WaitOne(millisecondTimeout);
                value = _interaction.resultValue;
                return _interactionHappened;
            }
        }

        public void Init()
        {
        }

        public MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout)
        {
            using (var consoleListener = new ConsoleListener())
            {
                _interactionHappened = false;
                _interaction = new Interaction();
                consoleListener.InteractionHandled += consoleListener_InteractionHandled;
                consoleListener.Start();
                Console.Write("Enter choice: ");
                getInput.WaitOne(millisecondTimeout);
                if (menu.ValidChoice(_interaction.resultValue))
                    return menu.GetMenuItem(_interaction.resultValue);
                else
                    return null;
            }
        }
    }

}
