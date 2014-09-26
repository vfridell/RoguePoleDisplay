using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Renderers;
using RoguePoleDisplay.Input;
using RoguePoleDisplay.Repositories;

namespace RoguePoleDisplay.Routines
{
    [RoutineType(RoutineType.Awake)]
    class FavoriteNumber : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var memory = Memory.GetInstance();
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            Interaction i = face.RememberSingleValue("What's your favorite number?");
            if(!memory.PlayerLoggedIn())
            {
                face.Talk("Interesting.");
                face.Talk("I don't know you.");
                face.SlowTalk("Or do I...");
                return MakeRoutineResult(i);
            }
            else
            {
                Interaction favNum = memory.Remember("What's your favorite number?", memory.CurrentPlayer);
                if(favNum.resultValue == i.resultValue)
                {
                    string counterKey = "FavNumberRuns";
                    Interaction count = memory.Remember(counterKey, memory.CurrentPlayer);
                    if (null == count)
                    {
                        memory.AddToMemory(new Interaction() { displayText = counterKey, resultValue = 1 });
                        face.Talk("Interesting.");
                        face.Talk(string.Format("I'll remember that {0}.", memory.CurrentPlayer.Name));
                        return MakeRoutineResult(favNum);
                    }
                    else
                    {
                        switch(count.resultValue)
                        {
                            case 1:
                                face.Talk("That's what I thought.");
                                break;
                            case 2:
                                face.Talk("Yup.");
                                break;
                            case 3:
                                face.Talk("Consistant.");
                                break;
                            case 4:
                                face.Talk("Powerful.");
                                break;
                            case 5:
                                face.Talk("Punctual.");
                                break;
                            case 6:
                                face.Talk("Material.");
                                break;
                            case 7:
                                face.Talk("Maternal.");
                                break;
                            case 8:
                                face.Talk("Fondue.");
                                break;
                            case 9:
                                face.Talk("Fondon't.");
                                break;
                            case 10:
                                face.Talk("Fuck.");
                                break;
                            default:
                                face.Talk("After 10 of anything", "I lose interest.");
                                break;
                        }
                        count.resultValue++;
                        memory.AddToMemory(count);
                    }
                }
                else
                {
                    face.Talk("Oh");
                    Interaction changedIt = face.YesNo("You changed it?");
                    if (changedIt.playerAnswer == Interaction.Answer.Yes)
                    {
                        face.Talk("You're so", "complicated.");
                    }
                    else if (changedIt.playerAnswer == Interaction.Answer.No)
                    {
                        face.Talk("Interesting...");
                        face.Talk("", "(Liar)", 2000);
                    }
                    else
                    {
                        face.Talk("Whatever");
                        face.Talk("I can ignore", "you too.");
                    }
                    return MakeRoutineResult(changedIt);
                }
            }
            return MakeRoutineResult(i);
        }
    }
}
