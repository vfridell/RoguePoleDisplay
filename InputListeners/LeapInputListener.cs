using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Leap;
using log4net;
using RoguePoleDisplay.Helpers;
using RoguePoleDisplay.Renderers;

namespace RoguePoleDisplay.InputListeners
{
    public class LeapInputListener : Listener, IObservable<InputData>
    {
        ILog _log = LogManager.GetLogger("RoguePoleDisplay");

        public int LastNumFingers { get; private set; }
        public int NumFingersAverage => _fingersHistogram.Mode();
        public InputData LastXmitLeapData = new InputData() { LastNumEntered = 0 };

        // Consistency means we have collected enough frames 
        // and the average (mode) value of those frames is equal to the last three frames collected
        public bool ConsistentNumFingers => _fingersHistogram.Count >= _consistentFramesNum && _fingersHistogram.ToList().Reverse<int>().Take(3).All(i => i == LastNumFingers);

        private IScreenRenderer _renderer { get; set; }
        private readonly int _queueLength;
        private readonly int _consistentFramesNum;
        private List<IObserver<InputData>> _observers = new List<IObserver<InputData>>();
        private ConcurrentQueue<int> _fingersHistogram = new ConcurrentQueue<int>();

        public void ResetTracking()
        {
            LastNumFingers = 0;
            LastXmitLeapData = new InputData() { LastNumEntered = 0 };
            _fingersHistogram = new ConcurrentQueue<int>();
        }

        public LeapInputListener(IScreenRenderer renderer)
        {
            _queueLength = 200;
            _consistentFramesNum = (int)Math.Floor(_queueLength * .33d);
            ResetTracking();
            _renderer = renderer;
        }

        public LeapInputListener(int queueLength, IScreenRenderer renderer)
        {
            _queueLength = queueLength;
            _consistentFramesNum = (int)Math.Floor(_queueLength * .33d);
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

                // if we saw a hand in the frame, it counts as at least one finger
                LastNumFingers = Math.Max(1, numFingers);

                while (_fingersHistogram.Count >= _queueLength) _fingersHistogram.TryDequeue(out int _);

                InputData leapData = new InputData() { LastNumEntered = LastNumFingers };

                lock (_observers)
                {
                    // send the current num fingers observed
                    foreach (var observer in _observers) observer.OnNext(leapData);
                }
            }

            // if we've achieved consistency, signal the observers that we are done
            if (ConsistentNumFingers)
            {
                _log.Debug($"QueueLength: {_queueLength}");
                _log.Debug($"ConsistentNumFingers: {ConsistentNumFingers} ");
                _log.Debug($"Last {_consistentFramesNum} FingersAverage: {NumFingersAverage}");
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

        public static int GetQueueLength(int millisecondTimeout)
        {
            return (int)Math.Min(millisecondTimeout * .02d, 300);
        }
    }
}
