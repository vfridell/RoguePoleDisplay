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
    [RoutineType(RoutineType.Awake)]
    class Directions : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            Interaction i = face.YesNo("Are you lost?");
            if (i.playerAnswer == Interaction.Answer.DidNotAnswer)
            {
                face.Talk("Nevermind");
                face.Talk("I'm Talking", "to myself");
                face.SlowTalk("again...");
                return MakeRoutineResult(i);
            }
            else if (i.playerAnswer == Interaction.Answer.No)
            {
                face.Talk("OK");
                face.Talk("Just checking");
                face.Talk("'cause I thought", "you looked...");
                face.Talk("you know", millisecondTimeout: 2000);
                face.Talk("", "kinda lost");
                return MakeRoutineResult(i);
            }
            else
            {
                Interaction looking = face.YesNo("Need the bathroom?");
                if(looking.playerAnswer == Interaction.Answer.Yes)
                {
                    face.Talk("It's right there", "          =========>", 10000);
                    face.SlowTalk("By the way");
                    Interaction brNeed = face.TwoChoices("Which one?", "One", "Two");
                    if(brNeed.playerAnswer == Interaction.Answer.DidNotAnswer)
                    {
                        face.Talk("Sorry.", "Too personal, I know");
                    }
                    else
                    {
                        face.Talk("Hope it all", "comes out ok");
                    }
                    return MakeRoutineResult(looking);
                }

                looking = face.YesNo("Need Tech Support?");
                if (looking.playerAnswer == Interaction.Answer.Yes)
                {
                    face.Talk("Go that way", "<==========         ");
                    face.Talk("Until you see", "<==========         ");
                    face.Talk("Big glass doors", "<==========         ", 10000);
                    face.Talk("You can't miss it");
                    return MakeRoutineResult(looking);
                }

                looking = face.YesNo("Need books?");
                if(looking.playerAnswer == Interaction.Answer.Yes)
                {
                    face.Talk("Look anywhere but", "the first floor.");
                    face.Talk("They are kinda", "taking those away.");
                    face.Talk("Something to do", "with the internet.");
                    face.Talk("I never get on that", "internet thing");
                    face.Talk("Too complicated", ";-)");
                    return MakeRoutineResult(looking);
                }

                face.Talk("Sorry, I don't know", "much else.");
                return MakeRoutineResult(i);
            }
        }
    }
}
