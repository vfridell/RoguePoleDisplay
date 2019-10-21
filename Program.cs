using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Leap;
using RoguePoleDisplay.Helpers;
using RoguePoleDisplay.Repositories;
using RoguePoleDisplay.Routines;

namespace RoguePoleDisplay
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.InitalizeLog4Net();
            StateOfMind stateOfMind = new StateOfMind();
            Task gameLoopTask = Task.Run(() => stateOfMind.BecomeSelfAware());
            Task.WaitAll(gameLoopTask);
        }
    }
}
