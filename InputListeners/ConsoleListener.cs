using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RoguePoleDisplay.InputListeners
{
    /// <summary>
    /// Uses a thread to allow for a timeout on Console.Readline
    /// </summary>
    public class ConsoleListener : InputListener
    {
        public ConsoleListener()
        {
        }

        protected override void Listen()
        {
            int result;
            var key = Console.ReadKey(true);
            if (int.TryParse(key.KeyChar.ToString(), out result)) OnInteractionHandled(new InteractionEventArgs(result));
        }
    }
}
