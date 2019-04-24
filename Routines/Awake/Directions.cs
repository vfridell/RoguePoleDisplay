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
    class Directions : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            using (var memory = new Memory())
            {
                Interaction i = face.YesNo(memory, "Are you lost?");
                if (i.PlayerAnswer == Interaction.Answer.DidNotAnswer)
                {
                    face.Talk(memory, "Nevermind");
                    face.Talk(memory, "I'm Talking", "to myself");
                    face.SlowTalk(memory, "again...");
                    return MakeRoutineResult(memory, i);
                }
                else if (i.PlayerAnswer == Interaction.Answer.No)
                {
                    face.Talk(memory, "OK");
                    face.Talk(memory, "Just checking");
                    face.Talk(memory, "'cause I thought", "you looked...");
                    face.Talk(memory, "you know", millisecondTimeout: 2000);
                    face.Talk(memory, "", "kinda lost");
                    return MakeRoutineResult(memory, i);
                }
                else
                {
                    Interaction looking = face.YesNo(memory, "Need the bathroom?");
                    if (looking.PlayerAnswer == Interaction.Answer.Yes)
                    {
                        face.Talk(memory, "It's right there", "          =========>", 10000);
                        face.SlowTalk(memory, "By the way");
                        Interaction brNeed = face.TwoChoices(memory, "Which one?", "One", "Two");
                        if (brNeed.PlayerAnswer == Interaction.Answer.DidNotAnswer)
                        {
                            face.Talk(memory, "Sorry.", "Too personal, I know");
                        }
                        else
                        {
                            face.Talk(memory, "Hope it all", "comes out ok");
                        }
                        return MakeRoutineResult(memory, looking);
                    }

                    looking = face.YesNo(memory, "Need Tech Support?");
                    if (looking.PlayerAnswer == Interaction.Answer.Yes)
                    {
                        face.Talk(memory, "Go that way", "<==========         ");
                        face.Talk(memory, "Until you see", "<==========         ");
                        face.Talk(memory, "Big glass doors", "<==========         ", 10000);
                        face.Talk(memory, "You can't miss it");
                        return MakeRoutineResult(memory, looking);
                    }

                    looking = face.YesNo(memory, "Need books?");
                    if (looking.PlayerAnswer == Interaction.Answer.Yes)
                    {
                        face.Talk(memory, "Look anywhere but", "the first floor.");
                        face.Talk(memory, "They are kinda", "taking those away.");
                        face.Talk(memory, "Something to do", "with the internet.");
                        face.Talk(memory, "I never get on that", "internet thing");
                        face.Talk(memory, "Too complicated", ";-)");
                        return MakeRoutineResult(memory, looking);
                    }

                    looking = face.YesNo(memory, "Need the ARC?");
                    if (looking.PlayerAnswer == Interaction.Answer.Yes)
                    {
                        face.Talk(memory, "Go that way", "<==========         ");
                    ///////////////////////01234567890123456789////////////////////
                        face.Talk(memory, "Then head down", "the stairs. VVV");
                        face.Talk(memory, "It's on the B-level", "");
                        face.Talk(memory, "Wish I was on B...", "");
                        face.Talk(memory, "So much cooler", " down there.");
                        return MakeRoutineResult(memory, looking);
                    }

                    face.Talk(memory, "Sorry, I don't know", "much else.");
                    return MakeRoutineResult(memory, i);
                }
            }
        }
    }
}
