using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Leap;

namespace RoguePoleDisplay
{
    class Program
    {
        static void Main(string[] args)
        {

            PoleDisplay p = PoleDisplay.GetInstance();
            p.Initialize();

            p.RPS10_Power_switch();


            //while (true)
            //{
            //    p.Write("Oh my gosh");
            //}

            //StateOfMind stateOfMind = new StateOfMind();
            //Task gameLoopTask = Task.Run(() => stateOfMind.RunRoutine());

            //Task.WaitAll(gameLoopTask);

            //Console.WriteLine("Press Enter to quit...");
            //Console.ReadLine();
        }
    }
}
