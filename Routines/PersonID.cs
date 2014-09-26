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
    class PersonID : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            Memory memory = Memory.GetInstance();
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            face.ResetIncrementer();

            face.Talk("I'm wondering if we've met before.");
            if (memory.GetKnownPlayers().Count > 0)
            {
                face.SlowTalk("Lets see...");
                foreach (Player player in memory.GetKnownPlayers())
                {
                    Interaction answer = face.GetSingleValue(player.Question, millisecondTimeout: 30000);
                    if (answer.resultValue == player.Answer)
                    {
                        face.Talk(string.Format("Hey, {0}!", player.Name));
                        face.Talk("I knew you'd be back.");
                        memory.CurrentPlayer = player;
                        answer.player = player;
                        return MakeRoutineResult(answer);
                    }
                    face.Talk("Oh.", millisecondTimeout: 2000);
                    face.TalkInCircles(5000, "No", "That's not right", "Nope");
                    Interaction knowYou = memory.Remember("Do I know you?", null);
                    if (null == knowYou) knowYou = face.YesNo("Do I know you?");
                    if (knowYou.playerAnswer == Interaction.Answer.No)
                    {
                        face.Talk("Well, no wonder.");
                        knowYou.success = false;
                        return MakeRoutineResult(knowYou);
                    }
                    face.Talk("Well then, let's maybe try another");
                }
                face.Talk("Actually, I don't think we've met.");
            }
            else
            {
                face.Talk("But I guess that's not possible");
            }
            return MakeRoutineResult(new Interaction() { success = false, resultValue = (int)Interaction.Answer.No });
        }
    }
}
