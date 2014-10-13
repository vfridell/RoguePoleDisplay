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
    //[RoutineType(RoutineType.Awake)]
    class PoopyTalk : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                Interaction i = face.TwoChoices(memory, "Cake or Pie?", "Cake", "Pie");
                face.Talk(memory, "AHHHHH!", "A zombie!");
                if (string.IsNullOrEmpty(i.resultText))
                {
                    face.Talk(memory, "You were eaten");
                }
                else
                {
                    face.Talk(memory, "Let's get the", "grenades");
                    face.Talk(memory, string.Format("{0} grenades", i.resultText));
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
