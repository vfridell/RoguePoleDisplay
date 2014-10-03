//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Leap;

//namespace RoguePoleDisplay.InputListeners
//{
//    public class LeapInputListener : InputListener
//    {
//        private int _numFingers;
//        private Controller _leapController;

//        public LeapInputListener()
//        {
//            SecondsBetweenInteractions = 0.50f;
//        }

//        public LeapInputListener(float secondsBetweenInteractions)
//        {
//            SecondsBetweenInteractions = secondsBetweenInteractions;
//        }

//        public float SecondsBetweenInteractions { get; set; }

//        public override void Start()
//        {
//            _leapController = new Controller();
//            base.Start();
//        }

//        public override void Stop()
//        {
//            base.Stop();
//            lock(_leapController) _leapController.Dispose();
//        }

//        protected override void Listen()
//        {
//            int oldNumFingers = _numFingers;
//            _numFingers = 0;

//            if (!_leapController.IsConnected) return;

//            Thread.Sleep((int)(SecondsBetweenInteractions * 1000));

//            var fingerCountList = new List<int>();
//            int framesToProcess = (int)Math.Ceiling((_leapController.Frame().CurrentFramesPerSecond * SecondsBetweenInteractions));
//            int framesProcessed = 0;
//            for (int i = 0; i < framesToProcess; i++)
//            {
//                Frame frame = _leapController.Frame(i);
//                if (frame.IsValid)
//                {
//                    fingerCountList.Add(CheckFrame(frame));
//                    framesProcessed++;
//                }
//            }

//            _numFingers = Mode(fingerCountList);

//            OnInteractionHandled(new InteractionEventArgs(_numFingers));
//        }

//        private class FingerCount : IComparable<FingerCount>
//        {
//            public int numFingers;
//            public int count;
//            public FingerCount(int fingers, int num) { numFingers = fingers; count = num; }

//            public int CompareTo(FingerCount other)
//            {
//                return count.CompareTo(other.count);
//            }
//        }

//        /// <summary>
//        /// Return the most frequently occurring value in a list
//        /// </summary>
//        /// <param name="intList"></param>
//        /// <returns></returns>
//        private int Mode(List<int> intList)
//        {
//            // get a list of counts
//            List<FingerCount> sortedByCount = new List<FingerCount>();
//            for (int num = 0; num <= 10; num++)
//                sortedByCount.Add(new FingerCount(num, intList.Count(i => i == num)));

//            sortedByCount.Sort();
//            return sortedByCount.Last().numFingers;
//        }

//        private int CheckFrame(Frame frame)
//        {
//            int fingerCount = 0;
//            if (!frame.Hands.IsEmpty)
//            {
//                // Get the first hand
//                Hand hand1 = frame.Hands[0];
//                Hand hand2 = frame.Hands[1];

//                // Check if the hand has any fingers
//                FingerList fingers = hand1.Fingers;
//                if (!fingers.IsEmpty)
//                {
//                    fingerCount = fingers.Count;
//                }

//                fingers = hand2.Fingers;
//                if (!fingers.IsEmpty)
//                {
//                    fingerCount += fingers.Count;
//                }
//            }
//            return fingerCount;
//        }
//    }
//}
