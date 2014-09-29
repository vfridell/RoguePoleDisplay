using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Input
{
    public class InputFactory
    {
        private static IGetInput _input;

        public static IGetInput GetPreferredInput()
        {
            if (null == _input)
            {
                //_input = new ConsoleInput();
                _input = new LeapInput();
                _input.Init();
            }
            return _input;
        }
    }
}
