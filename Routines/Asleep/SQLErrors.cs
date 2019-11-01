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
    public class SQLErrors : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                Interaction i = new Interaction();
                foreach (var kvp in SQLErrorData.SQLErrors)
                {
                    if (CheckForAnything(memory, face, kvp.Key, kvp.Value.Substring(0, 20), 1000, out i)) return MakeRoutineResult(memory, i);
                }
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
