using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RoguePoleDisplay.Routines;
using RoguePoleDisplay.Repositories;

namespace RoguePoleDisplay
{
    public class StateOfMind 
    {
        private static object _stateChangeLock = new object();

        public StateOfMind()
        {
        }

        public void BecomeSelfAware()
        {
            Memory memory = Memory.GetInstance();

            do
            {
                RoutineType routineType = memory.CurrentState.GetNextRoutineType();
                Routine currentRoutine = RoutineFactory.GetRoutine(routineType);
                RoutineResult result = currentRoutine.Run();
                if (routineType == RoutineType.Login 
                    && !memory.PlayerLoggedIn()
                    && result.FinalState != RoutineFinalState.Abandoned)
                {
                    currentRoutine = RoutineFactory.GetCreateLoginRoutine();
                    result = currentRoutine.Run();
                }

                Console.WriteLine("routine result was " + result.FinalState.ToString());

                if(memory.CurrentState.CheckForStateChange())
                {
                    Console.WriteLine("state changed to {0}", memory.CurrentState.GetType().Name);
                }
            } while (true);
        }

    }
}
