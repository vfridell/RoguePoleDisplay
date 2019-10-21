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
    [RoutineType(RoutineType.Login, 0)]
    public class PersonID : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.ResetIncrementer();

                face.Talk(memory, "I'm wondering if", "we've met before.");
                if (memory.GetKnownPlayers().Count > 0)
                {
                    face.SlowTalk(memory, "Lets see...");
                    foreach (Player player in memory.GetKnownPlayers())
                    {
                        Interaction answer = face.GetSingleValue(memory, player.QuestionLine1, player.QuestionLine2, millisecondTimeout: 30000);
                        if (answer.PlayerAnswer == Interaction.Answer.DidNotAnswer)
                        {
                            face.Talk(memory, "I'm talking", "to myself.");
                            return MakeRoutineResult(memory, answer);
                        }
                        else if (answer.ResultValue == player.Answer)
                        {
                            face.Talk(memory, string.Format("Hey, {0}!", player.Name));
                            face.Talk(memory, "I knew you'd", "be back.");
                            memory.SetCurrentPlayer(player);
                            answer.Player = player;
                            return MakeRoutineResult(memory, answer);
                        }
                        face.Talk(memory, "Oh.", millisecondTimeout: 2000);
                        face.TalkInCircles(memory, 5000, "No", "That's not right", "Nope");
                        Interaction knowYou = memory.Remember("Do I know you?", "");
                        if (null == knowYou) knowYou = face.YesNo(memory, "Do I know you?");
                        if (knowYou.PlayerAnswer == Interaction.Answer.DidNotAnswer)
                        {
                            face.Talk(memory, "I'm talking", "to myself.");
                            return MakeRoutineResult(memory, answer);
                        }
                        if (knowYou.PlayerAnswer == Interaction.Answer.No)
                        {
                            face.Talk(memory, "Well, no wonder.");
                            knowYou.Success = false;
                            return MakeRoutineResult(memory, knowYou);
                        }
                        face.Talk(memory, "Well then, let's", "maybe try another");
                    }
                    face.Talk(memory, "Actually, I don't", "think we've met.");
                }
                else
                {
                    face.Talk(memory, "But I guess", "that's not possible");
                }
                return MakeRoutineResult(memory, new Interaction() { Success = false, ResultValue = (int)Interaction.Answer.No });
            }
        }
    }
}
