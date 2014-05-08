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
    class CreatePlayer : Routine
    {
        public override void Init()
        {
            routineType = RoutineType.PersonID;
        }

        public override Interaction Run()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());

            face.Talk("Let's think of a secret");
            string question = Memory.GetInstance().RandomQuestionWithNoAnswer;
            if (string.IsNullOrEmpty(question))
            {
                face.Talk("Sorry, I'm full on friends");
                return new Interaction() { success = false };
            }

            Interaction newPlayer = face.RememberSingleValue(question, longTerm: true);
            face.Talk("Great!");
            return newPlayer;
        }
    }
}
