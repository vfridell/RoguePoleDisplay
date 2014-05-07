using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using RoguePoleDisplay.LeapListeners;

namespace RoguePoleDisplay.Screens
{
    class BasicScreen : IScreen
    {
        public string line1 { get; set; }
        public string line2 { get; set; }

        public BasicScreen(string pline1, string pline2)
        {
            line1 = pline1;
            line2 = pline2;
        }

        public void Draw(PoleDisplay poleDisplay, GameState gameState, Controller controller)
        {
            poleDisplay.Clear();
            poleDisplay.Write(line1.PadRight(20).Substring(0, 20));
            poleDisplay.Write(line2.PadRight(20).Substring(0, 20));
        }
    }
}
