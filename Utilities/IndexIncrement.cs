using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Utilities
{
    public class IndexIncrement
    {
        private int _current;
        private int _max;
        private bool _loop;

        public IndexIncrement(int max, bool loop)
        {
            _current = -1;
            _max = max;
            _loop = loop;
        }

        public int Next
        {
            get
            {
                _current++;
                if (_loop && _current >= _max)
                    _current = 0;

                return _current;
            }
        }
    }
}
