using RoguePoleDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoguePoleDisplay.Repositories;

namespace RoguePoleDisplay
{
    public class Interaction
    {
        public Interaction()
        {
            timestamp = DateTime.Now;
            player = Memory.GetInstance().CurrentPlayer;
        }

        public Interaction(int resultVal)
            : base()
        {
            resultValue = resultVal;
        }

        public DateTime timestamp;
        public int resultValue = -1;
        public string displayText = "";
        public bool success = true;
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
