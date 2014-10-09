using RoguePoleDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoguePoleDisplay.InputListeners
{
    public class InteractionEventArgs : System.EventArgs
    {
        public readonly Interaction interaction;
        public InteractionEventArgs(Interaction pinteraction)
        {
            interaction = pinteraction;
        }

        public InteractionEventArgs(int result)
        {
            interaction = new Interaction(result);
        }
    }

    public abstract class InputListener : IDisposable
    {
        private Thread _inputThread;
        public event EventHandler<InteractionEventArgs> InteractionHandled;

        protected virtual void OnInteractionHandled(InteractionEventArgs e)
        {
            if (InteractionHandled != null) InteractionHandled(this, e);
        }

        public virtual void Start()
        {
            KeepGoing = true;
            _inputThread = new Thread(ListenLoop);
            _inputThread.IsBackground = true;
            _inputThread.Start();
        }

        public virtual void Stop()
        {
            KeepGoing = false;
        }

        protected abstract void Listen();

        private object _kgLock = new object();
        private bool _keepGoing = false;
        protected bool KeepGoing
        {
            get
            {
                lock (_kgLock) { return _keepGoing; }
            }

            set
            {
                lock (_kgLock) { _keepGoing = value; }
            }
        }

        protected virtual void ListenLoop()
        {
            try
            {
                while (KeepGoing)
                {
                    Listen();
                }
            }
            catch (Exception ex)
            {
                // TODO how to handle?
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
