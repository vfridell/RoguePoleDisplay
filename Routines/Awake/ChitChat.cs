using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Renderers;
using RoguePoleDisplay.Input;

namespace RoguePoleDisplay.Routines
{
    [RoutineType(RoutineType.Awake)]
    class ChitChat : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            face.Talk("So happy to be here!");
            Interaction i = face.RememberSingleValue("What's new?");
            face.Talk("Uh huh.");
            return MakeRoutineResult(i);
        }
    }
}
