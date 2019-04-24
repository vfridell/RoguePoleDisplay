using RoguePoleDisplay;
using RoguePoleDisplay.Models;
using RoguePoleDisplay.Repositories;
using RoguePoleDisplay.Routines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPoleDisplayInput
{
    class Program
    {
        static void Main(string[] args)
        {
                do
                {
                    RoutineType routineType = Memory.CurrentState.GetNextRoutineType();
                    Routine currentRoutine = RoutineFactory.GetRoutine(routineType);
                    RoutineResult result = currentRoutine.Run();
                    if (routineType == RoutineType.Login
                        && !Memory.PlayerLoggedIn()
                        && result.FinalState != RoutineFinalState.Abandoned)
                    {
                        currentRoutine = RoutineFactory.GetCreateLoginRoutine();
                        result = currentRoutine.Run();
                    }

                    Console.WriteLine("routine result was " + result.FinalState.ToString());

                    if (Memory.CurrentState.CheckForStateChange())
                    {
                        Console.WriteLine("state changed to {0}", Memory.CurrentState.GetType().Name);
                        Console.WriteLine("Reason: {0}", ConsciousnessState.StateChangeReason);
                    }
                } while (true);
        }
    }
}
