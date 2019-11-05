using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Renderers
{
    public interface IScreenRenderer
    {
        void Init();
        void Write(string line1, string line2);
        void SlowType(string line1, string line2, int msTypingDelay);
        void Clear();
        void DisplayMenu(Menu menu);
        void WritePosition(char c, int x, int y);
        void WriteProgressIndicator(int total, int start, int current);
        void Fade(char c, int millisecondsBetweenSteps);
        void Scroll(string line1, string line2, int msScrollDelay, int numLoops);
    }
}
