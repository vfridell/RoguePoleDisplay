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
    [RoutineType(RoutineType.Awake)]
    class PoopyTalk : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            Interaction i = face.TwoChoices("Cake or Pie?", "Cake", "Pie");
            face.Talk("AHHHHH!", "A zombie!");
            if (string.IsNullOrEmpty(i.resultText))
            {
                face.Talk("You were eaten");
            }
            else
            {
                face.Talk("Let's get the", "grenades");
                face.Talk(string.Format("{0} grenades", i.resultText));
            }
            return MakeRoutineResult(i);
        }
    }
}
