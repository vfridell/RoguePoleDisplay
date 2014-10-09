using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Renderers;
using RoguePoleDisplay.Input;
using RoguePoleDisplay.Models;

namespace RoguePoleDisplay.Routines
{
    [RoutineType(RoutineType.Awake)]
    class PimpMyself : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            face.Talk("Tweet me", "@BellarmineIT");
            face.Talk("I may just reply.", "@BellarmineIT", 10000);
            face.Talk("No guarantees", "", 1000);
            Interaction i = face.YesNo("Will you tweet me?");
            return MakeRoutineResult(i);
        }
    }
}
