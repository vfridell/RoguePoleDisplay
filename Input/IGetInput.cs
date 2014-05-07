using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Input
{
    public interface IGetInput
    {
        void Init();
        int GetInteger(int millisecondTimeout);
        bool TryGetInteger(out int value, int millisecondTimeout);
        MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout);
    }
}
