using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Routines;

namespace RoguePoleDisplay
{
    public class StateOfMind
    {
        public StateOfMind()
        {
        }

        public void RunRoutine()
        {
            Routine idRoutine = new PersonID();
            Routine secondOne;
            idRoutine.Init();
            Interaction result = idRoutine.Run();
            if (string.IsNullOrEmpty(result.playerName))
                secondOne = new CreatePlayer();
            else
                secondOne = new ChitChat();

            secondOne.Init();
            secondOne.Run();

        }
        
    }
}
