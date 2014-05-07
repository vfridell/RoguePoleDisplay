using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using RoguePoleDisplay.LeapListeners;

namespace RoguePoleDisplay.Screens
{
    interface IScreen
    {
        void Draw(PoleDisplay poleDisplay, GameState gameState, Controller controller);
    }
}
