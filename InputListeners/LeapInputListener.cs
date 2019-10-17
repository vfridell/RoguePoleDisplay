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
    public class LeapInputListener : Listener, IObservable<InputData>
    {
        public int LastNumFingers { get; private set; }
        public int NumFingersAverage => _fingersHistogram.Mode();
        public bool ConsistentNumFingers => NumFingersAverage != 0 && NumFingersAverage == _fingersHistogram.ToList().Reverse<int>().Take(_consistentFramesNum).Mode();
        public InputData LastXmitLeapData = new InputData() { LastNumEntered = 0 };

        private IScreenRenderer _renderer { get; set; }
        private int _queueLength = 300;
        private int _consistentFramesNum = 200;
        private List<IObserver<InputData>> _observers = new List<IObserver<InputData>>();
        private ConcurrentQueue<int> _fingersHistogram = new ConcurrentQueue<int>();

        public void ResetTracking()
        {
            LastNumFingers = 0;
            LastXmitLeapData = new InputData() { LastNumEntered = 0 };
            for (int i = 0; i<_queueLength; i++) _fingersHistogram.Enqueue(0);
        }

        public LeapInputListener(IScreenRenderer renderer)
        {
            ResetTracking();
            _renderer = renderer;
        }

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
            }

            while (_fingersHistogram.Count >= _queueLength) _fingersHistogram.TryDequeue(out int _);

            InputData leapData = new InputData() { LastNumEntered = LastNumFingers };
            if (LastNumFingers != 0)
            {
                lock (_observers)
                {
                    foreach (var observer in _observers) observer.OnNext(leapData);
                }
            }
            if (ConsistentNumFingers) // && !LastXmitLeapData.Equals(leapData))
            {
                LastXmitLeapData = new InputData() { LastNumEntered = NumFingersAverage };
                lock (_observers)
                {
                    foreach (var observer in _observers) observer.OnCompleted();
                }
            }
        }

        public IDisposable Subscribe(IObserver<InputData> observer)
        {
            lock (_observers)
            {
                if (!_observers.Contains(observer))
                {
                    _observers.Add(observer);
                }
                return new Unsubscriber<InputData>(_observers, observer);
            }
        }

    }
}
