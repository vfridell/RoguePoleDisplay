using RoguePoleDisplay.Repositories;
using RoguePoleDisplay.Routines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.ConsciousnessStates
{
    public class Friendly : Awake
    {
        public override bool CheckForStateChange()
        {
            if (Memory.PlayerLoggedIn())
            {
                StateChangeReason = "Player logged in";
                ChangeState(AwakeState);
                return true;
            }
            return base.CheckForStateChange();
        }

        public override RoutineType GetNextRoutineType()
        {
            return RoutineType.Login;
        }
    }
}
