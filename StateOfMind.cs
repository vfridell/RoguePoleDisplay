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

            PoleDisplay p = PoleDisplay.GetInstance();
            p.Initialize();

            do
            {
                RoutineType routineType = memory.CurrentState.GetNextRoutineType();
                Routine currentRoutine = RoutineFactory.GetRoutine(routineType);
                RoutineResult result = currentRoutine.Run();
                if (routineType == RoutineType.Login && !memory.PlayerLoggedIn())
                {
                    currentRoutine = RoutineFactory.GetCreateLoginRoutine();
                    result = currentRoutine.Run();
                }

                if(memory.CurrentState.CheckForStateChange())
                {
                    Console.WriteLine("state changed to {0}", memory.CurrentState.GetType().Name);
                }
            } while (true);
        }

    }
}
