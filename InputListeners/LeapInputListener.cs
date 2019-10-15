using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Leap;
using RoguePoleDisplay.Helpers;
using RoguePoleDisplay.Renderers;

namespace RoguePoleDisplay.InputListeners
{
    public class LeapInputListener : Listener
    {
        public int LastNumFingers { get; private set; }
        public int NumFingersAverage => _fingersHistogram.Mode();
        private IScreenRenderer _renderer { get; set; }

        private ConcurrentQueue<int> _fingersHistogram = new ConcurrentQueue<int>();

        public LeapInputListener(IScreenRenderer renderer)
        {
            _renderer = renderer;
        }

        public event Action<int, IScreenRenderer> OnRenderRefresh;


        public override void OnFrame(Controller controller)
        {
            Frame frame = controller.Frame();

            if (!frame.Hands.IsEmpty)
            {
                int numFingers = 0;
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

                _fingersHistogram.Enqueue(numFingers);
                LastNumFingers = numFingers;

                _renderer.WritePosition(LastNumFingers.ToString()[0], 19, 1);
            }
            else
            {
                _fingersHistogram.Enqueue(0);
            }

            while (_fingersHistogram.Count >= 9) _fingersHistogram.TryDequeue(out int _);
        }
    }
}
