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

namespace RoguePoleDisplay.Input
{
    public class LeapInput : IGetInput
    {
        private TaskCompletionSource<int> _taskSource = new TaskCompletionSource<int>();
        private Controller _leapController;

        public void Init()
        {
        }

        public int GetInteger(int millisecondTimeout)
        {
            throw new NotImplementedException();
        }

        public bool TryGetInteger(out int value, int millisecondTimeout)
        {
            throw new NotImplementedException();
        }

        public MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout)
        {
            new Thread(() =>
                {
                    int fingers = GetFingers(5, menu.NumberOfChoices, 
                        (choice, renderer) =>
                        {
                            menu.Highlight(choice);
                            renderer.DisplayMenu(menu);
                        });
                    _taskSource.SetResult(fingers);
                }).Start();
            Task.Delay(millisecondTimeout).ContinueWith((t) => { if (_taskSource.Task.Status == TaskStatus.Running) _taskSource.SetCanceled(); });
            return menu.GetMenuItem(_taskSource.Task.Result);
        }

        private int GetFingers(int secondsToHold, int numberOfChoices, Action<int, IScreenRenderer> onRefresh)
        {
            IScreenRenderer renderer = RendererFactory.GetPreferredRenderer();
            using (Controller leapController = new Controller())
            {
                int numFingers = 0;
                DateTime choiceTime = ResetTimer(secondsToHold);
                while (DateTime.Now < choiceTime)
                {
                    int oldNumFingers = numFingers;

                    // Get the most recent frame and report some basic information
                    Frame frame = leapController.Frame();

                    if (!frame.Hands.IsEmpty)
                    {
                        // Get the first hand
                        Hand hand = frame.Hands[0];

                        // Check if the hand has any fingers
                        FingerList fingers = hand.Fingers;
                        if (!fingers.IsEmpty)
                        {
                            numFingers = fingers.Count;
                        }
                    }

                    if (oldNumFingers != numFingers)
                    {
                        onRefresh(numFingers, renderer);
                        choiceTime = ResetTimer(secondsToHold);
                    }

                    //if (_numFingers >= 1 && _numFingers <= numberOfChoices && DateTime.Now > _choiceTime)
                    //{
                    //    //                gameState.currentState = new Chosen(_menu.GetMenuItem(_numFingers));
                    //}
                }
                return numFingers;
            }
        }

        private DateTime ResetTimer(int secondsFromNow)
        {
            return DateTime.Now.AddSeconds(secondsFromNow);
        }


    }
}
