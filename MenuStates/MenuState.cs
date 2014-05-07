using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using RoguePoleDisplay.LeapListeners;

namespace RoguePoleDisplay.MenuStates
{
    public interface MenuState
    {
        void OnLeapFrame(PoleDisplay poleDisplay, GameState gameState, Controller controller);
    }
}
