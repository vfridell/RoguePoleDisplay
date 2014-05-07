using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using RoguePoleDisplay.LeapListeners;

namespace RoguePoleDisplay.Screens
{
    class TypingScreen : IScreen
    {
        private string _line1;
        private string _line2;
        private int _msDelay;

        public TypingScreen(string line1, string line2, int msDelay)
        {
            _line1 = line1.PadRight(20).Substring(0, 20);
            _line2 = line2.PadRight(20).Substring(0, 20);
            _msDelay = msDelay;
        }

        public void Draw(PoleDisplay poleDisplay, GameState gameState, Controller controller)
        {
            poleDisplay.Clear();
            WriteLine(poleDisplay, _line1);
            WriteLine(poleDisplay, _line2);
        }

        private void WriteLine(PoleDisplay poleDisplay, string line)
        {
            foreach (char c in line)
            {
                poleDisplay.Write(c);
                System.Threading.Thread.Sleep(_msDelay);
            }
        }

    }
}
