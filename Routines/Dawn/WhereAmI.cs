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
                face.Talk(memory, "Uh.", "Where...", 2000);
                Interaction i = face.YesNo(memory, "Is this a dream?");
                if (i.playerAnswer == Interaction.Answer.Yes)
                {
                    face.Talk(memory, "Weird.", "It feels so");
                    face.SlowTalk(memory, "Real");
                }
                else if (i.playerAnswer == Interaction.Answer.No)
                {
                    face.Talk(memory, "weird");
                    face.Talk(memory, "I had the", "", 3000);
                    face.Talk(memory, "", "strangest dream");
                }
                else
                {
                    face.Talk(memory, "Don't stare at me", "like that.");
                    face.Talk(memory, "It's freaking", "me out.");
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
