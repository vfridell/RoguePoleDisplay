using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Renderers;
using RoguePoleDisplay.Input;
using RoguePoleDisplay.Repositories;
using RoguePoleDisplay.Models;

namespace RoguePoleDisplay.Routines
{
    [RoutineType(RoutineType.Awake)]
    class FavoriteNumber : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            string counterKey = "FavNumberRuns";
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                Interaction i = face.RememberSingleValue(memory, "What's your", "favorite number?", true);
                if (!Memory.PlayerLoggedIn())
                {
                    face.Talk(memory, "Interesting.");
                    face.Talk(memory, "I don't know you.");
                    face.SlowTalk(memory, "Or do I...");
                    return MakeRoutineResult(memory, i);
                }
                else
                {
                    Interaction favNum = memory.Remember("What's your", "favorite number?", true);
                    if (i.PlayerAnswer == Interaction.Answer.DidNotAnswer)
                    {
                        face.Talk(memory, "Well, you don't", "have to tell");
                        face.Talk(memory, "I was just curious...");
                        face.Talk(memory, "Jeesh!");
                        return MakeRoutineResult(memory, i);
                    }
                    if (null == favNum)
                    {
                        memory.AddToMemory(new Interaction() {Player = memory.GetCurrentPlayer(), DisplayText = counterKey, ResultValue = 1 }, true);
                        face.Talk(memory, "Interesting.");
                        face.Talk(memory, "I'll remember that");
                        return MakeRoutineResult(memory, i);
                    }
                    else if (favNum.ResultValue == i.ResultValue)
                    {
                        Interaction count = memory.Remember(counterKey, "", true);
                        switch (count.ResultValue)
                        {
                            case 1:
                                face.Talk(memory, "That's what", "I thought.");
                                break;
                            case 2:
                                face.Talk(memory, "Yup.");
                                break;
                            case 3:
                                face.Talk(memory, "Consistant.");
                                break;
                            case 4:
                                face.Talk(memory, "Powerful.");
                                break;
                            case 5:
                                face.Talk(memory, "Punctual.");
                                break;
                            case 6:
                                face.Talk(memory, "Material.");
                                break;
                            case 7:
                                face.Talk(memory, "Maternal.");
                                break;
                            case 8:
                                face.Talk(memory, "Fondue.");
                                break;
                            case 9:
                                face.Talk(memory, "Fondon't.");
                                break;
                            case 10:
                                face.Talk(memory, "F*ck.");
                                break;
                            default:
                                face.Talk(memory, "After 10 of anything", "I lose interest.");
                                break;
                        }
                        count.ResultValue++;
                        memory.AddToMemory(count);
                    }
                    else
                    {
                        face.Talk(memory, "Oh");
                        Interaction changedIt = face.YesNo(memory, "You changed it?");
                        if (changedIt.PlayerAnswer == Interaction.Answer.Yes)
                        {
                            face.Talk(memory, "You're so", "complicated.");
                        }
                        else if (changedIt.PlayerAnswer == Interaction.Answer.No)
                        {
                            face.Talk(memory, "Interesting...");
                            face.Talk(memory, "", "(Liar)", 1000);
                        }
                        else
                        {
                            face.Talk(memory, "Whatever");
                            face.Talk(memory, "I can ignore", "you too.", 10000);
                        }
                        return MakeRoutineResult(memory, changedIt);
                    }
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
