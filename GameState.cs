using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.MenuStates;
using Leap;

namespace RoguePoleDisplay
{
    public abstract class GameState
    {
        public MenuState currentState;

        public abstract void Update(PoleDisplay poleDisplay, Controller controller);
    }
}
