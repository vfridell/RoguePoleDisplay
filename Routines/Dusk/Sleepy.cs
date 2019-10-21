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
    [RoutineType(RoutineType.Dusk)]
    public class Sleepy : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.Talk(memory, "Man, I'm", "sleepy");
                Interaction i = face.YesNo(memory, "Are you sleepy?");
                if (i.PlayerAnswer == Interaction.Answer.Yes)
                {
                    face.Talk(memory, "Let's rest.");
                }
                else if (i.PlayerAnswer == Interaction.Answer.No)
                {
                    face.Talk(memory, "No? Well,", "I'll try");
                    face.Talk(memory, "To stay", "awake.");
                    face.Talk(memory, "* Yawn *");
                }
                else
                {
                    face.Talk(memory, "Guess nobody cares");
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
