using RoguePoleDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Models
{
    public enum RoutineFinalState { Abandoned, One, Two };

    public class RoutineResult
    {
        public long Id { get; private set; }
        public string RoutineType { get; set; }
        public DateTime Timestamp { get; set; }
        public RoutineFinalState FinalState { get; set; }
        public Interaction FinalInteraction { get; set; }
    }
}
