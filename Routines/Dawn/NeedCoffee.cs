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
    [RoutineType(RoutineType.Dawn)]
    class NeedCoffee : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            face.Talk("* Yawn *", "", 7000);
            face.Talk("* smack *", " * smack *", 7000);
            face.Talk("errrrngh", " * stretch *", 3000);
            face.Talk("...");
            face.Talk("I need", "coffee");
            Interaction i = face.YesNo("Got any coffee?");
            if(i.playerAnswer == Interaction.Answer.Yes)
            {
                face.Talk("If only", "I had a mouth.");
            }
            else
            {
                face.Talk("Doesn't matter", "anyway.");
                face.Talk("I can't drink.", "Not old enough.");
            }
            return MakeRoutineResult(i);
        }
    }
}
