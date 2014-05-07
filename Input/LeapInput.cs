using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoguePoleDisplay.Repositories;
using RoguePoleDisplay.Routines;
using Leap;

namespace RoguePoleDisplay.Input
{
    public class LeapInput : Listener, IGetInput
    {
        public override void OnInit(Controller controller)
        {

        }

        public override void OnFrame(Controller controller)
        {
            
        }

        public void Init()
        {
            
        }

        public int GetInteger(int millisecondTimeout)
        {
            throw new NotImplementedException();
        }

        public bool TryGetInteger(out int value, int millisecondTimeout)
        {
            throw new NotImplementedException();
        }

        public MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout)
        {
            throw new NotImplementedException();
        }
    }
}
