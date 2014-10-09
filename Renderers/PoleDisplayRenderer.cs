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

        public void Write(string line1, string line2)
        {
            line1 = line1.PadRight(20).Substring(0, 20);
            _poleDisplay.Write(line1);
            if (!string.IsNullOrEmpty(line2))
            {
                line2 = line2.PadRight(20).Substring(0, 20);
                _poleDisplay.Write(line2);
            }
        }

        public void SlowType(string line1, string line2, int msTypingDelay = 200)
        {
            foreach (char c in line1)
            {
                _poleDisplay.Write(c);
                System.Threading.Thread.Sleep(msTypingDelay);
            }
            foreach (char c in line2)
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
            _poleDisplay.WriteMenu(menu);
        }

        public void WritePosition(char c, int x, int y)
        {
            _poleDisplay.WritePos(c, x, y);
        }
    }
}
