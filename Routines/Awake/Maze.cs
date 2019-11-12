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
    public class Maze : Routine
    {
        public class Topic { public string name; public bool positive; }

        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                Interaction i = new Interaction();



                return MakeRoutineResult(memory, i);
            }
        }
    }
}
