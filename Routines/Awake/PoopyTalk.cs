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
    class PoopyTalk : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            Interaction i = face.YesNo("Do you like pie?");
            i = face.RememberSingleValue("AHHHHH!", "A zombie!", false, 10000);
            face.Talk("Let's get the", "grenades");
            face.Talk("Poopy grenades");
            return MakeRoutineResult(i);
        }
    }
}
