using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Renderers;
using RoguePoleDisplay.Input;
using RoguePoleDisplay.Models;
using RoguePoleDisplay.Repositories;

namespace RoguePoleDisplay.Routines
{
    //[RoutineType(RoutineType.Awake)]
    class PimpMyself : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.Talk(memory, "Tweet me", "@BellarmineIT");
                face.Talk(memory, "I may just reply.", "@BellarmineIT", 10000);
                face.Talk(memory, "No guarantees", "", 1000);
                Interaction i = face.YesNo(memory, "Will you tweet me?");
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
