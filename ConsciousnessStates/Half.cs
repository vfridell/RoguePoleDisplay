using RoguePoleDisplay.Repositories;
using RoguePoleDisplay.Routines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.ConsciousnessStates
{
    public class Half : ConsciousnessState
    {
        public override bool CheckForStateChange()
        {
            using (var memory = new Memory())
            {
                if (Memory.LastState is Asleep &&
                    memory.LastRoutineCompleted())
                {
                    StateChangeReason = string.Format("Was asleep and last routine was completed");
                    ChangeState(AwakeState);
                    return true;
                }

                if (memory.LastRoutineAbandoned())
                {
                    StateChangeReason = string.Format("Last routine was abandoned");
                    ChangeState(AsleepState);
                    return true;
                }

                return false;
            }
        }

        public override RoutineType GetNextRoutineType()
        {
            using (var memory = new Memory())
                if (Memory.LastState is Awake)
                {
                    return RoutineType.Dusk;
                }
                else if (Memory.LastState is Asleep)
                {
                    return RoutineType.Dawn;
                }
                else
                {
                    throw new Exception(string.Format("Last Consciousness State not recognized: {0}", Memory.LastState.ToString()));
                }
        }
    }
}
