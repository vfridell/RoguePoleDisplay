using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay
{
    public enum RoutineFinalState { Abandoned, Success, Failure };

    public class RoutineResult
    {
        public Type RoutineType { get; set; }
        public DateTime Timestamp { get; set; }
        public RoutineFinalState FinalState { get; set; }
        public Interaction FinalInteraction { get; set; }
    }
}
