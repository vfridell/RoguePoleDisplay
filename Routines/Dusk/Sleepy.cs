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
    class Sleepy : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            face.Talk("Man, I'm", "sleepy");
            Interaction i = face.YesNo("Are you sleepy?");
            if (i.playerAnswer == Interaction.Answer.Yes)
            {
                face.Talk("Let's rest.");
            }
            else if(i.playerAnswer == Interaction.Answer.No)
            {
                face.Talk("No? Well,", "I'll try");
                face.Talk("To stay", "awake.");
                face.Talk("* Yawn *");
            }
            else
            {
                face.Talk("Guess nobody cares");
            }
            return MakeRoutineResult(i);
        }
    }
}
