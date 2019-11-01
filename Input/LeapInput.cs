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
    public class LeapInput : IGetInput, IObserver<InputData>
    {
        private Leap.Controller _leapController;
        private EventWaitHandle _waitHandle = new AutoResetEvent(false);
        private InputData _lastObservedInputData;

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
            _waitHandle.Reset();
            _lastObservedInputData = new InputData();
            _interactiveRenderAction = (choice) =>
            {
                RendererFactory.GetPreferredRenderer().WritePosition(choice.ToString()[0], 19, 1);
            };

            int queueLength = LeapInputListener.GetQueueLength(millisecondTimeout);
            using (var leapInputListener = new LeapInputListener(queueLength, RendererFactory.GetPreferredRenderer()))
            using (leapInputListener.Subscribe(this))
            {
                _leapController.AddListener(leapInputListener);
                if (_waitHandle.WaitOne(millisecondTimeout))
                {
                    value = leapInputListener.LastXmitLeapData.LastNumEntered;
                }
                else // timed out
                {
                    // use the last observed value
                    value = _lastObservedInputData.LastNumEntered;
                }
                _leapController.RemoveListener(leapInputListener);
            }

            return value > 0;
        }

        public MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout)
        {
            _waitHandle.Reset();
            _lastObservedInputData = new InputData();
            _interactiveRenderAction = (choice) =>
            {
                menu.Highlight(choice);
                RendererFactory.GetPreferredRenderer().DisplayMenu(menu);
            };

            int value;
            int queueLength = LeapInputListener.GetQueueLength(millisecondTimeout);
            using (var leapInputListener = new LeapInputListener(queueLength, RendererFactory.GetPreferredRenderer()))
            using (leapInputListener.Subscribe(this))
            {
                _leapController.AddListener(leapInputListener);
                if (_waitHandle.WaitOne(millisecondTimeout))
                {
                    value = leapInputListener.LastXmitLeapData.LastNumEntered;
                }
                else // timed out
                {
                    // use the last observed value
                    value = _lastObservedInputData.LastNumEntered;
                }
                _leapController.RemoveListener(leapInputListener);
            }

            if (!menu.ValidChoice(value))
                return null;
            else
                return menu.GetMenuItem(value);
        }

        public void OnNext(InputData value)
        {
            _lastObservedInputData = value;
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
