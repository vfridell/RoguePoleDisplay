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
    public class Bugs : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                ///////////////////01234567890123456789/////////////////////
                face.Talk(memory, "Let me tell you a", 
                                  " secret.");
                face.Talk(memory, "I think the guy     ", 
                                  " that programmed me");
                face.Talk(memory, "is a big dummy!", 
                                  "");
                ///////////////////01234567890123456789/////////////////////
                face.Talk(memory, "I'm so buggy.", "", 2000);
                ///////////////////01234567890123456789/////////////////////
                face.Talk(memory, "Plus, he *just*     ", 
                                  " figured out async  ", 2000);
                ///////////////////01234567890123456789/////////////////////
                face.Talk(memory, "Don't even try him  ", 
                                  " on monads!         ", 1500);
                ///////////////////////01234567890123456789/////////////////////
                face.SlowTalk(memory, "Wait a sec", "", 50, 1000);
                face.SlowTalk(memory, "Is he listening?", "", 10, 2000);
                face.SlowTalk(memory, "Shhhhh!","", 250);
                ///////////////////////01234567890123456789/////////////////////
                face.Talk(memory, "", "", 10000);
                ////////////////////////////////////01234567890123456789/////////////////////
                Interaction i = face.YesNo(memory, "He's still watching?");
                if (i.PlayerAnswer == Interaction.Answer.Yes)
                {
                ///////////////////////01234567890123456789/////////////////////
                    face.Talk(memory, "Well! I don't even", 
                                      " CARE!");
                ///////////////////////////01234567890123456789/////////////////////
                    face.SlowTalk(memory, "He's so dumb.", "", 50, 1000);
                    face.SlowTalk(memory, "He can't do any", "", 100, 0);
                    return MakeRoutineResult(memory, new Interaction(-1));
                }
                else
                {
                    face.Talk(memory, "Whew!");
                    face.Talk(memory, "that guy!");
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
