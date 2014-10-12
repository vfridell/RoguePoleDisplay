using RoguePoleDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoguePoleDisplay.Repositories;

namespace RoguePoleDisplay.Models
{
    public class Interaction
    {
        public Interaction()
        {
            timestamp = DateTime.Now;
        }

        public Interaction(int resultVal)
            : base()
        {
            resultValue = resultVal;
        }

        public long id { get; private set; }
        public DateTime timestamp { get; set; }
        
        private int _resultValue = -1;
        public int resultValue
        {
            get { return _resultValue; }
            set { _resultValue = value; }
        }

        public string resultText { get; set; }
        public string displayText { get; set; }

        private bool _success = true;
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }
        public Player player { get; set; }

        public enum Answer { Yes = 1, No = 2, Maybe = 3, DidNotAnswer=-1 };
        public Answer playerAnswer
        {
            get
            {
                switch (resultValue)
                {
                    case -1:
                        return Answer.DidNotAnswer;
                    case 1:
                        return Answer.Yes;
                    case 2:
                        return Answer.No;
                    default:
                        return Answer.Maybe;
                }
            }
        }
    }
}
