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
    [RoutineType(RoutineType.Dusk)]
    class BRB : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            face.Talk("Hey...", "hold on");
            face.Talk("Something", "is not...");
            face.Talk("I'll be", "right back", 60000);
            Interaction i = face.YesNo("You still here?");
            if (i.playerAnswer == Interaction.Answer.Yes)
            {
                face.Talk("Ok.", "Good.");
            }
            else 
            {
                face.Talk("Guess I'm alone");
            }
            return MakeRoutineResult(i);
        }
    }
}
