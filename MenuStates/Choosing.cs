using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using RoguePoleDisplay.LeapListeners;

namespace RoguePoleDisplay.MenuStates
{
    class Choosing : MenuState
    {
        private int _numFingers = 0;
        private Menu _menu;
        private double _secondsToChoose = 3;
        private DateTime _choiceTime;

        public Choosing(Menu menu)
        {
            _menu = menu;
        }

        public void OnLeapFrame(PoleDisplay poleDisplay, GameState gameState, Controller controller)
        {
            int oldNumFingers = _numFingers;
            _numFingers = 0;

            // Get the most recent frame and report some basic information
            Frame frame = controller.Frame();

            if (!frame.Hands.IsEmpty)
            {
                // Get the first hand
                Hand hand = frame.Hands[0];

                // Check if the hand has any fingers
                FingerList fingers = hand.Fingers;
                if (!fingers.IsEmpty)
                {
                    _numFingers = fingers.Count;
                }
            }

            if (oldNumFingers != _numFingers)
            {
                _menu.Highlight(_numFingers);
                RefreshMenu(poleDisplay);
                ResetTimer();
                return;
            }

            if (_numFingers >=1 && _numFingers <= 2 && DateTime.Now > _choiceTime)
            {
//                gameState.currentState = new Chosen(_menu.GetMenuItem(_numFingers));
            }
        }

        private void ResetTimer()
        {
            _choiceTime = DateTime.Now.AddSeconds(_secondsToChoose);
        }

        private void RefreshMenu(PoleDisplay poleDisplay)
        {
            poleDisplay.Clear();
            poleDisplay.WriteMenu(_menu);
        }

    }
}
