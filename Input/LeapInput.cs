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
        private Leap.Controller _leapController;
        private EventWaitHandle _waitHandle = new AutoResetEvent(false);

        public void Init()
        {
            _leapController = new Controller();
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
            };

            using (var leapInputListener = new LeapInputListener(RendererFactory.GetPreferredRenderer()))
            using (leapInputListener.Subscribe(this))
            {
                _leapController.AddListener(leapInputListener);
                if (_waitHandle.WaitOne(millisecondTimeout))
                {
                    value = leapInputListener.LastXmitLeapData.LastNumEntered;
                }
                else // timed out
                {
                    value = 0;
                }
                _leapController.RemoveListener(leapInputListener);
            }

            // give the user a chance to see the final choice
            _interactiveRenderAction(value);

            return value > 0;
        }

        public MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout)
        {
            _interactiveRenderAction = (choice) =>
            {
                menu.Highlight(choice);
                RendererFactory.GetPreferredRenderer().DisplayMenu(menu);
            };

            int value;
            using (var leapInputListener = new LeapInputListener(RendererFactory.GetPreferredRenderer()))
            using (leapInputListener.Subscribe(this))
            {
                _leapController.AddListener(leapInputListener);
                if (_waitHandle.WaitOne(millisecondTimeout))
                {
                    value = leapInputListener.LastXmitLeapData.LastNumEntered;
                }
                else // timed out
                {
                    value = 0;
                }
                _leapController.RemoveListener(leapInputListener);
            }

            // give the user a chance to see the final choice
            _interactiveRenderAction(value);

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
