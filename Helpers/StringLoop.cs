using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Helpers
{
    public class StringLoop
    {
        private string _value;

        public StringLoop(string value)
        {
            _value = value;
        }

        public string Substring(int index, int length)
        {
            StringBuilder result = new StringBuilder();
            int endIndex = index + Math.Max(0, length - 1);
            int startIndex = index;
            int addressableStartIndex = GetAddressableIndex(startIndex);
            int addressableEndIndex = GetAddressableIndex(endIndex);
            
            if(startIndex == addressableStartIndex && endIndex == addressableEndIndex)
            {
                return _value.Substring(index, length);
            }

            int numCopies = (length / _value.Length) - 1;
            for (int i = 0; i < numCopies; i++) result.Append(_value);

            if(numCopies > 0 || addressableEndIndex < addressableStartIndex)
            {
                result.Insert(0, _value.Substring(addressableStartIndex));
                result.Append(_value.Substring(0, addressableEndIndex + 1));
                return result.ToString();
            }
            else
            {
                return _value.Substring(addressableStartIndex, addressableEndIndex - addressableStartIndex);
            }
        }

        private int GetAddressableIndex(int index)
        {
            return index % _value.Length;
        }
    }

}
