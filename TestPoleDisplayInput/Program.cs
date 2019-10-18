using Leap;
using RoguePoleDisplay;
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
