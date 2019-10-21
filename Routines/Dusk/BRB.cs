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
    public class BRB : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.Talk(memory, "Hey...", "hold on");
                face.Talk(memory, "Something", "is not...");
                face.Talk(memory, "I'll be", "right back", 2000);
                for (int j = 1; j <= 1000; j *= 10)
                {
                    face.Fade(memory, '/', j);
                    face.Fade(memory, '|', j);
                    face.Fade(memory, '\\', j);
                    face.Fade(memory, '|', j);
                }
                Interaction i = face.YesNo(memory, "You still here?");
                if (i.PlayerAnswer == Interaction.Answer.Yes)
                {
                    face.Talk(memory, "Ok.", "Good.");
                }
                else
                {
                    face.Talk(memory, "Guess I'm alone");
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
