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
    class PersonID : Routine
    {
        public override void Init()
        {
            routineType = RoutineType.PersonID;
        }

        public override Interaction Run()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            face.ResetIncrementer();

            face.Talk("I'm wondering if we've met before.");
            if (Memory.GetInstance().QuestionsWithAnswers.Count > 0)
            {
                face.SlowTalk("Lets see...");
                foreach (string question in Memory.GetInstance().QuestionsWithAnswers)
                {
                    Interaction answer = face.GetSingleValue(question, millisecondTimeout: 30000);
                    Interaction player = Memory.GetInstance().Remember(answer.displayText, true);
                    if (answer.resultValue == player.resultValue)
                    {
                        face.Talk(string.Format("Hey, {0}!", player.playerName));
                        face.Talk("I knew you'd be back.");
                        return player;
                    }
                    face.Talk("Oh.", millisecondTimeout: 2000);
                    face.TalkInCircles(5000, "No", "That's not right", "Nope");
                    Interaction knowYou = Memory.GetInstance().Remember("Do I know you?");
                    if (null == knowYou) knowYou = face.YesNo("Do I know you?");
                    if (knowYou.playerAnswer == Interaction.Answer.No)
                    {
                        face.Talk("Well, no wonder.");
                        return new Interaction() { success = false };
                    }
                    face.Talk("Well then, let's maybe try another");
                }
                face.Talk("Actually, I don't think we've met.");
            }
            else
            {
                face.Talk("But I guess that's not possible");
            }
            return new Interaction() { success = false };
        }
    }
}
