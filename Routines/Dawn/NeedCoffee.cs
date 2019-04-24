using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Renderers;
using RoguePoleDisplay.Input;
using RoguePoleDisplay.Models;
using RoguePoleDisplay.Repositories;

namespace RoguePoleDisplay.Routines
{
    [RoutineType(RoutineType.Dawn)]
    class NeedCoffee : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.Talk(memory, "* Yawn *", "", 7000);
                face.Talk(memory, "* smack *", " * smack *", 7000);
                face.Talk(memory, "errrrngh", " * stretch *", 3000);
                face.Talk(memory, "...");
                face.Talk(memory, "I need", "coffee");
                Interaction i = face.YesNo(memory, "Got any coffee?");
                if (i.PlayerAnswer == Interaction.Answer.Yes)
                {
                    face.Talk(memory, "If only", "I had a mouth.");
                }
                else
                {
                    face.Talk(memory, "Doesn't matter", "anyway.");
                    face.Talk(memory, "I can't drink.", "Not old enough.");
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
