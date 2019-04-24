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
    class Broken : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.Talk(memory, "I'm going to try", " something new.");
                face.Talk(memory, "Not sure if it's", " going to work.");

                Interaction i = face.GetSingleValue(memory, "Gimme some input!");
                face.Fade(memory, i.ResultValue.ToString()[0], 1);
                Interaction work = face.YesNo(memory, "Did it work?");
                if (work.PlayerAnswer == Interaction.Answer.Yes)
                {
                    face.Talk(memory, "Hmm.", "");
                    face.Talk(memory, "You can tell me", " the truth.");
                    face.Talk(memory, "I can handle it.", "");
                    face.Talk(memory, "Let me try this...");
                    face.Talk(memory, "", "", 10000);
                    ///////////////////01234567890123456789////////////////////
                    face.Talk(memory, "      ULTIMATE      ",
                                      "     TECHNOLOGY     ", 10000);
                    return MakeRoutineResult(memory, new Interaction(-1));
                }
                else if (work.PlayerAnswer == Interaction.Answer.No)
                {
                    face.Talk(memory, "Darn!");
                }
                else
                {
                    face.Talk(memory, "Hello?");
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
