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
        private Leap.Controller _leapController;
        public void Init()
        {
            _leapController = new Controller();
            _leapController.SetPolicyFlags(Controller.PolicyFlag.POLICY_BACKGROUND_FRAMES);
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
                     Console.WriteLine(choice.ToString());
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
                     Console.WriteLine(choice.ToString());
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
            IScreenRenderer renderer = RendererFactory.GetPreferredRenderer();
            Stopwatch sw = new Stopwatch();
            sw.Start();
                
            int numFingers = -1;
            DateTime choiceTime = ResetTimer(secondsToHold);
            while (DateTime.Now < choiceTime )
            {
                // Get the most recent frame and report some basic information
                Frame frame = _leapController.Frame();

                if (sw.ElapsedMilliseconds >= millisecondTimeout)
                {
                    Console.WriteLine(string.Format("FPS: {0}", frame.CurrentFramesPerSecond));
                    sw.Stop();
                    return numFingers;
                }

                int oldNumFingers = numFingers;

                if (!frame.Hands.IsEmpty)
                {
                    // Get the hands
                    Hand hand1 = frame.Hands[0];
                    Hand hand2 = frame.Hands[1];

                    // Check if the hand has any fingers
                    FingerList fingers = hand1.Fingers.Extended();
                    if (!fingers.IsEmpty)
                    {
                        numFingers = fingers.Count;
                    }

                    fingers = hand2.Fingers.Extended();
                    if (!fingers.IsEmpty)
                    {
                        numFingers += fingers.Count;
                    }
                }

                if (oldNumFingers != numFingers)
                {
                    onRefresh(numFingers, renderer);
                    choiceTime = ResetTimer(secondsToHold);
                }
                else
                {
                    //onRefresh((int)frame.CurrentFramesPerSecond, renderer);
                }
            }

            return numFingers;
        }

        private DateTime ResetTimer(int secondsFromNow)
        {
            return DateTime.Now.AddSeconds(secondsFromNow);
        }
    }
}
