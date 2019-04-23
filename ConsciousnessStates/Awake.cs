using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Repositories;
using RoguePoleDisplay.Routines;

namespace RoguePoleDisplay.ConsciousnessStates
{
    public class Awake : ConsciousnessState
    {
        public override bool CheckForStateChange()
        {
            using (var memory = new Memory())
            {
                if (!Memory.PlayerLoggedIn())
                {
                    Random random = new Random();
                    int num = random.Next(1, 5);
                    if (num >= 3)
                    {
                        StateChangeReason = string.Format("Player not logged in.  Random chance.");
                        ChangeState(FriendlyState);
                        return true;
                    }
                }

                double threshold = new Random().NextDouble() * 10;
                if (memory.LastRoutineAbandoned() ||
                   memory.RoutinesCompletedSinceStateBegan() > threshold)
                {
                    StateChangeReason = string.Format("Routine abandoned ({0}) or routines completed ({1}) greater than threshold value {2}",
                                   memory.LastRoutineAbandoned(),
                                   memory.RoutinesCompletedSinceStateBegan(),
                                   threshold);
                    ChangeState(HalfState);
                    return true;
                }

                return false;
            }
        }

        public override RoutineType GetNextRoutineType()
        {
            return RoutineType.Awake;
        }
    }
}
