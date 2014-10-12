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
    [RoutineType(RoutineType.CreateLogin, 0)]
    class CreatePlayer : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());

                face.Talk(memory, "Let's think", "of a secret");
                Player player = memory.GetPlayerWithNoAnswer();
                if (null == player)
                {
                    face.Talk(memory, "Sorry,", "I'm full on friends");
                    return MakeRoutineResult(memory, new Interaction() { success = false });
                }

                Interaction newPlayer = face.RememberSingleValue(memory, player.QuestionLine1, player.QuestionLine2, longTerm: true);
                if (newPlayer.playerAnswer == Interaction.Answer.DidNotAnswer)
                {
                    face.SlowTalk(memory, "Well");
                    face.Talk(memory, "We don't", "have to be friends");
                    face.Talk(memory, "I guess...");
                }
                else
                {
                    player.Answer = newPlayer.resultValue;
                    memory.SetCurrentPlayer(player);
                    newPlayer.player = player;
                    face.Talk(memory, "Great!");
                    face.Talk(memory, "I'll call you", player.Name, 8000);
                    face.Talk(memory, "Remember both the", "Q & A for next time", 8000);
                    face.Talk(memory, "And I'll", "remember you!", 8000);
                }
                return MakeRoutineResult(memory, newPlayer);
            }
        }
    }
}
