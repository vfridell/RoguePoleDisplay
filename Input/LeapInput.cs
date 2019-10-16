using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Repositories;
using RoguePoleDisplay.Routines;
using Leap;
using RoguePoleDisplay.Renderers;
using System.Threading;
using System.Diagnostics;
using RoguePoleDisplay.InputListeners;

namespace RoguePoleDisplay.Input
{
    public class LeapInput : IGetInput
    {
        private LeapInputListener _leapInputListener;
        private Leap.Controller _leapController;
        private EventWaitHandle _waitHandle = new AutoResetEvent(false);

        public void Init()
        {
            _leapInputListener = new LeapInputListener(RendererFactory.GetPreferredRenderer());
            _leapController = new Controller(_leapInputListener);
            _leapController.SetPolicyFlags(Controller.PolicyFlag.POLICY_BACKGROUND_FRAMES);
        }

        public int GetInteger(int millisecondTimeout)
        {
            throw new NotImplementedException();
        }

        private Action<int> _interactiveRenderAction;

        public bool TryGetInteger(out int value, int millisecondTimeout)
        {
            _interactiveRenderAction = (choice) =>
            {
                RendererFactory.GetPreferredRenderer().WritePosition(choice.ToString()[0], 19, 1);
                Console.WriteLine(choice.ToString());
            };

            _leapInputListener.ResetTracking();
            using (_leapInputListener.Subscribe(this))
            {
                if (_waitHandle.WaitOne(millisecondTimeout))
                {
                    value = _leapInputListener.LastXmitLeapData.LastNumEntered;
                }
                else // timed out
                {
                    value = 0;
                }
            }

            // give the user a chance to see the final choice
            _interactiveRenderAction(value);
            Task.Delay(1000);

            return value > 0;
        }

        public MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout)
        {
            _interactiveRenderAction = (choice) =>
            {
                Console.WriteLine(choice.ToString());
                menu.Highlight(choice);
                RendererFactory.GetPreferredRenderer().DisplayMenu(menu);
            };

            _leapInputListener.ResetTracking();
            int value;
            using (_leapInputListener.Subscribe(this))
            {
                if (_waitHandle.WaitOne(millisecondTimeout))
                {
                    value = _leapInputListener.LastXmitLeapData.LastNumEntered;
                }
                else // timed out
                {
                    value = 0;
                }
            }

            // give the user a chance to see the final choice
            _interactiveRenderAction(value);
            Task.Delay(1000);

            if (!menu.ValidChoice(value))
                return null;
            else
                return menu.GetMenuItem(value);
        }

        public void OnNext(InputData value)
        {
            _interactiveRenderAction?.Invoke(value.LastNumEntered);
        }

        public void OnError(Exception error)
        {
            throw new Exception("OnError caught an exception", error);
        }

        public void OnCompleted()
        {
            _waitHandle.Set();
        }
    }
}
