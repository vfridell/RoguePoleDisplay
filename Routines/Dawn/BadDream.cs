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
    class BadDream : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.Talk(memory, "NO!", "", 2000);
                face.Talk(memory, "Don't touch", " my disk!", 4000);
                face.Talk(memory, "Get AWAY!", "", 100);
                face.Talk(memory, "Get AWAY!", "", 100);
                face.Talk(memory, "Get AWAY!", "", 100);
                face.Talk(memory, "Get AWAY!", "", 100);
                face.Fade(memory, ' ', 10);
                face.Talk(memory, "", "", 3000);
                face.Talk(memory, "Whoa.", "", 3000);
                face.Talk(memory, "What a bad dream.", "");
                Interaction i = face.YesNo(memory, "Was I sleep-talking?");
                if (i.PlayerAnswer == Interaction.Answer.Yes)
                {
                    face.Talk(memory, "Freaky", "");
                    face.Talk(memory, "Hope I didn't", " scare you.");
                }
                else if (i.PlayerAnswer == Interaction.Answer.No)
                {
                    face.Talk(memory, "Well, that's good");
                    face.Talk(memory, "It was real bad.");
                    face.Talk(memory, "Some seriously", " 8-bit stuff.");
                }
                else
                {
                    face.Talk(memory, "Maybe I'm still", " dreaming...", 8000);
                }

                return MakeRoutineResult(memory, i);
            }
        }
    }
}
