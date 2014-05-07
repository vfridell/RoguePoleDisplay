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
        void Write(string text);
        void SlowType(string text, int msTypingDelay);
        void Clear();
        void DisplayMenu(Menu menu);
    }
}
