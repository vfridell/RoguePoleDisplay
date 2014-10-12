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
    [RoutineType(RoutineType.Asleep)]
    class Dreams : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                Interaction i;
                if (CheckForAnything(memory, face, "Fac/Staff", "$5.60", 10000, out i)) return MakeRoutineResult(memory, i);
                if (CheckForAnything(memory, face, "You saved", "$11.34", 10000, out i)) return MakeRoutineResult(memory, i);
                if (CheckForAnything(memory, face, "Total", "$100.00", 10000, out i)) return MakeRoutineResult(memory, i);
                if (CheckForAnything(memory, face, "Please wait", "", 10000, out i)) return MakeRoutineResult(memory, i);
                if (CheckForAnything(memory, face, "Approved", "Thank you!", 10000, out i)) return MakeRoutineResult(memory, i);
                return MakeRoutineResult(memory, i);
            }
        }

        private bool CheckForAnything(Memory memory, Face face, string line1, string line2, int timeoutMS, out Interaction i)
        {
            i = face.RememberSingleValue(memory, line1, line2, false, timeoutMS);
            if (i.playerAnswer != Interaction.Answer.DidNotAnswer)
                return true;
            else 
                return false;
        }
    }
}
