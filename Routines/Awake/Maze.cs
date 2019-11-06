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
                Topic[] topics = {  new Topic() { name = "Ebola", positive=false},
                                new Topic() { name = "football game", positive=true},
                                new Topic() { name = "weather", positive=true},
                             };

                Random rand = new Random();
                int index = rand.Next(0, 3);

                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                Interaction i = face.RememberSingleValue(memory, "How bout that", topics[index].name);
                face.Talk(memory, "Uh huh.");
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
