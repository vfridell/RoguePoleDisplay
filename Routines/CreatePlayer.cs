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
            Memory memory = Memory.GetInstance();
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());

            face.Talk("Let's think", "of a secret");
            Player player = memory.GetPlayerWithNoAnswer();
            if (null == player)
            {
                face.Talk("Sorry,", "I'm full on friends");
                return MakeRoutineResult(new Interaction() { success = false });
            }

            Interaction newPlayer = face.RememberSingleValue(player.QuestionLine1, player.QuestionLine2, longTerm: true);
            if (newPlayer.playerAnswer == Interaction.Answer.DidNotAnswer)
            {
                face.SlowTalk("Well");
                face.Talk("We don't", "have to be friends");
                face.Talk("I guess...");
            }
            else
            {
                player.Answer = newPlayer.resultValue;
                memory.CurrentPlayer = player;
                newPlayer.player = player;
                face.Talk("Great!");
                face.Talk("I'll call you", player.Name, 8000);
                face.Talk("Remember both the", "Q & A for next time", 8000);
                face.Talk("And I'll", "remember you!", 8000);
            }
            return MakeRoutineResult(newPlayer);
        }
    }
}
