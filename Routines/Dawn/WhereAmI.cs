using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Renderers;
using RoguePoleDisplay.Input;

namespace RoguePoleDisplay.Routines
{
    [RoutineType(RoutineType.Dawn)]
    class WhereAmI : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            face.Talk("Uh.", "Where...", 8000);
            Interaction i = face.RememberSingleValue("Where", "am I?");
            face.Talk("weird");
            face.Talk("I had the", "strangest", 6000);
            face.Talk("strangest", "dream");
            return MakeRoutineResult(i);
        }
    }
}
