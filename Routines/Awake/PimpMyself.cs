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
    [RoutineType(RoutineType.Awake)]
    class PimpMyself : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.Talk(memory, "Tweet me", "@BellarmineIT");
                face.Talk(memory, "I may just reply.", "@BellarmineIT", 10000);
                face.Talk(memory, "No guarantees", "", 1000);
                Interaction i = face.YesNo(memory, "Will you tweet me?");
                switch (i.playerAnswer)
                {
                    case Interaction.Answer.Yes:
                        face.Talk(memory, "Cool!");
                        face.Talk(memory, "Oh.", "", 1000);
                        face.Talk(memory, "Use the word", " 'Aardvark'");
                        face.Talk(memory, "In your tweet", " for bonus points.");
                        face.Talk(memory, "(I love that word)", "", 3000);
                        break;
                    case Interaction.Answer.No:
                        face.Talk(memory, "That's ok.", "I understand.");
                        face.Talk(memory, "I'm more of the ", " 'lurker' type too.");
                        break;
                    case Interaction.Answer.Maybe:
                        face.Talk(memory, "Maybe?!");
                        face.Talk(memory, "Be decisive!");
                        face.Talk(memory, "If you want to, ", " I mean.");
                        break;
                    default:
                        face.Talk(memory, "Crickets");
                        face.Talk(memory, "", "not the same thing", 1000);
                        break;
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
