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
    [RoutineType(RoutineType.Dawn)]
    class WhereAmI : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.Talk(memory, "Uh.", "Where...", 8000);
                Interaction i = face.RememberSingleValue(memory, "Where", "am I?");
                face.Talk(memory, "weird");
                face.Talk(memory, "I had the", "strangest", 6000);
                face.Talk(memory, "strangest", "dream");
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
