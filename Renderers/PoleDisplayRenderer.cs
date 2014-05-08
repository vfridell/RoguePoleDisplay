using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Renderers
{
    public class PoleDisplayRenderer : IScreenRenderer
    {
        private PoleDisplay _poleDisplay { get { return PoleDisplay.GetInstance(); } }

        public void Init()
        {
            _poleDisplay.Initialize();
        }

        public void Write(string text)
        {
            //_line1 = line1.PadRight(20).Substring(0, 20);
            //_line2 = line2.PadRight(20).Substring(0, 20);
            _poleDisplay.Write(text);
        }

        public void SlowType(string text, int msTypingDelay = 200)
        {
            foreach (char c in text)
            {
                _poleDisplay.Write(c);
                System.Threading.Thread.Sleep(msTypingDelay);
            }
        }

        public void Clear()
        {
            _poleDisplay.Clear();
        }

        public void DisplayMenu(Menu menu)
        {
            throw new NotImplementedException();
        }
    }
}
