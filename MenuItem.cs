using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoguePoleDisplay
{
    public class MenuItem
    {
        private string _text;
        public string text 
        {
            get 
            {
                int padding;
                if (choiceNumber == 2 || choiceNumber == 4)
                    padding = 7;
                else
                    padding = 8;

                return _text.PadRight(padding).Substring(0, padding); 
            }
            set { _text = value; } 
        }

        public int choiceNumber;

        public bool highlight = false;

        public string choiceNumberAndText 
        {
            get 
            {
                if (highlight)
                    return string.Format("* {0}", text);
                else
                    return string.Format("{0} {1}", choiceNumber, text); 
            }
        }

        public string plainText
        {
            get
            {
                return _text;
            }
        }
    }
}
