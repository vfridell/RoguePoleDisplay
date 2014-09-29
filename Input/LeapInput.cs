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

namespace RoguePoleDisplay.Input
{
    public class LeapInput : IGetInput
    {

        public void Init()
        {
        }

        public int GetInteger(int millisecondTimeout)
        {
            throw new NotImplementedException();
        }

        public bool TryGetInteger(out int value, int millisecondTimeout)
        {
            TaskCompletionSource<int> taskSource = new TaskCompletionSource<int>();
            Task<int> t = taskSource.Task;
            Task.Factory.StartNew(() => {
                int fingers = GetFingers(5, 10, millisecondTimeout,
                 (choice, renderer) =>
                 {
                     renderer.WritePosition(choice.ToString()[0], 19, 1);
                 });
                taskSource.SetResult(fingers);
            });
            value = t.Result;
            return t.Result > 0;
        }

        public MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout)
        {
            TaskCompletionSource<int> taskSource = new TaskCompletionSource<int>();
            Task<int> t = taskSource.Task;
            Task.Factory.StartNew(() =>
            {
                int fingers = GetFingers(5, 10, millisecondTimeout,
                 (choice, renderer) =>
                 {
                     menu.Highlight(choice);
                     renderer.DisplayMenu(menu);
                 });
                taskSource.SetResult(fingers);
            });
            int value = t.Result;
            if (!menu.ValidChoice(value))
                return null;
            else
                return menu.GetMenuItem(value);
        }

        private int GetFingers(int secondsToHold, int numberOfChoices, int millisecondTimeout, Action<int, IScreenRenderer> onRefresh)
        {
            //IScreenRenderer renderer = RendererFactory.GetPreferredRenderer();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (Controller leapController = new Controller())
            {
                int numFingers = 0;
                DateTime choiceTime = ResetTimer(secondsToHold);
                while (DateTime.Now < choiceTime )
                {
                    if (sw.ElapsedMilliseconds >= millisecondTimeout)
                    {
                        sw.Stop();
                        return -1;
                    }

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
                        //onRefresh(numFingers, renderer);
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
