using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Renderers;
using RoguePoleDisplay.Input;

namespace RoguePoleDisplay.Routines
{
    //[RoutineType(RoutineType.Awake)]
    class PimpMyself : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            face.Talk("Tweet me", "@BellarmineIT");
            Interaction i = face.RememberSingleValue("I may just reply.", "@BellarmineIT", false, 10000);
            face.Talk("Poopy grenades", "", 1000);
            face.Talk("Sorry,", "won't happen again");
            return MakeRoutineResult(i);
        }
    }
}
