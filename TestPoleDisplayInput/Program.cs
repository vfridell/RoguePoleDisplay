using Leap;
using RoguePoleDisplay;
using RoguePoleDisplay.Helpers;
using RoguePoleDisplay.InputListeners;
using RoguePoleDisplay.Models;
using RoguePoleDisplay.Renderers;
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
                      //123456789012345678901234567890
                      //012345678901234567890123456789
            string s = "AN ILLEGAL XML CHARACTER hex-char WAS FOUND IN AN SQL/XML EXPRESSION OR FUNCTION ARGUMENT THAT BEGINS WITH STRING start-string";
            StringLoop sloop = new StringLoop(s);
            string empty = sloop.Substring(0, 0);
            string empty2 = sloop.Substring(900, 0);
            string normal = sloop.Substring(0, 10);
            string normal2 = sloop.Substring(10, 5);
            string endOver = sloop.Substring(s.Length - 5, 10);
            string overAndOver = sloop.Substring(5, (s.Length * 3) + 5);
            int shouldbe = (s.Length * 3) + 5;
            int itis = overAndOver.Length;

            Routine testRoutine = RoutineFactory.CreateAndInitRoutine(typeof(Nouns));
            testRoutine.Run();

        }

        static void LeapPoleTest()
        {
            IScreenRenderer _renderer = RendererFactory.GetPreferredRenderer();
            var _leapInputListener = new LeapInputListener(_renderer);
            var _leapController = new Controller(_leapInputListener);
            _leapController.SetPolicyFlags(Controller.PolicyFlag.POLICY_BACKGROUND_FRAMES);

            while (true)
            {
                Task.Delay(100);
                _renderer.WritePosition(_leapInputListener.NumFingersAverage.ToString()[0], 0, 0);
                if (_leapInputListener.ConsistentNumFingers) _renderer.WritePosition('C', 0, 1);
                else _renderer.WritePosition('X', 0, 1);
            }

        }
    }
}
