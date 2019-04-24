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
            Timestamp = DateTime.Now;
        }

        public Interaction(int resultVal)
            : this()
        {
            ResultValue = resultVal;
        }

        public virtual long Id { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual Player Player { get; set; }
        public virtual bool Success { get; set; }
        public virtual int ResultValue { get; set; }
        public virtual string ResultText { get; set; }
        public virtual string DisplayText { get; set; }

        public enum Answer { Yes = 1, No = 2, Maybe = 3, DidNotAnswer=-1 };
        public virtual Answer PlayerAnswer
        {
            get
            {
                switch (ResultValue)
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
