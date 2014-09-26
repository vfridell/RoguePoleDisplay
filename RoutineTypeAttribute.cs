using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Routines;

namespace RoguePoleDisplay
{
    [AttributeUsage(AttributeTargets.Class, Inherited=false)]
    public sealed class RoutineTypeAttribute : Attribute
    {
        public RoutineType routineType { get; set; }
        public int order { get; set; }

        public RoutineTypeAttribute(RoutineType pRoutineType, int routineOrder = 0)
        {
            routineType = pRoutineType;
            order = routineOrder;
        }
    }
}
