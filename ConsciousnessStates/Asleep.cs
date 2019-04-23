using RoguePoleDisplay.Repositories;
using RoguePoleDisplay.Routines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.ConsciousnessStates
{
    public class Asleep : ConsciousnessState
    {
        public override bool CheckForStateChange()
        {
            int interactionsNeededToBeginHalf;
            TimeSpan elapsed = DateTime.Now - _stateEntered;
            if (elapsed.Minutes >= 30)
            {
                interactionsNeededToBeginHalf = 3;
            }
            else if (elapsed.Minutes >= 15)
            {
                interactionsNeededToBeginHalf = 2;
            }
            else
            {
                interactionsNeededToBeginHalf = 1;
            }

            using (var memory = new Memory())
                if (memory.PlayerInteractionsSince(_stateEntered) >= interactionsNeededToBeginHalf)
                {
                    StateChangeReason = string.Format("Number of interactions ({0}) since {1} meets the threshold value of {2}",
                                                       memory.PlayerInteractionsSince(_stateEntered),
                                                       _stateEntered.ToShortTimeString(),
                                                       interactionsNeededToBeginHalf);
                    ChangeState(HalfState);
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public override RoutineType GetNextRoutineType()
        {
            return RoutineType.Asleep;
        }
    }
}
