using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Routines
{
    public abstract class Routine
    {
        public enum RoutineType { Sleeping, Dawn, Awake, Dusk, PersonID };
        public RoutineType routineType;

        public Routine() { }

        public abstract void Init();
        public abstract Interaction Run();
    }
}
