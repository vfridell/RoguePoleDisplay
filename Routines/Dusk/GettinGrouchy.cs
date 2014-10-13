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
    class GettinGrouchy : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.Talk(memory, "Hey!");
                Interaction i = face.YesNo(memory, "Seen my pager?");
                if (i.playerAnswer == Interaction.Answer.Yes)
                {
                ///////////////////////////01234567890123456789////////////////////
                    face.SlowTalk(memory, "Whaaaaaaaaaaaaaaaaa",
                                          "aaaaaaaaaaaaaaat!  ", 100, 2000);
                ///////////////////////01234567890123456789////////////////////
                    face.Talk(memory, "were we",
                                      " talking about?");
                ///////////////////////01234567890123456789////////////////////
                    face.Talk(memory, "Gettin sleepy.      ",
                                      " taking  a out.     ");
                ///////////////////////01234567890123456789////////////////////
                    face.Talk(memory, "Hard to stay awake. " ,
                                      " taking  a out.");
                ///////////////////////01234567890123456789////////////////////
                    face.Talk(memory, "Are you still here??" ,
                                      "    in     one.");
                ///////////////////////01234567890123456789////////////////////
                    face.Talk(memory, "" ,
                                      "     n     o  .");
                ///////////////////////01234567890123456789////////////////////
                    return MakeRoutineResult(memory, new Interaction(-1));
                }
                else if (i.playerAnswer == Interaction.Answer.No)
                {
                ///////////////////////01234567890123456789////////////////////
                    face.Talk(memory, "No? ", 
                                      "It was around here");
                ///////////////////////01234567890123456789////////////////////
                    face.Talk(memory, "I'm waiting", 
                                      " for a page");
                ///////////////////////01234567890123456789////////////////////
                    face.Talk(memory, "Gotta call someone  ");
                ///////////////////////01234567890123456789////////////////////
                    face.Talk(memory, "out");
                ///////////////////////01234567890123456789////////////////////
                }
                else
                {
                ///////////////////////01234567890123456789////////////////////
                    face.Talk(memory, "Could have sworn",
                                      " someone was there..");
                ///////////////////////01234567890123456789////////////////////
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
