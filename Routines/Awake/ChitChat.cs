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
    class ChitChat : Routine
    {
        public class Topic { public string name; public bool positive; }

        protected override RoutineResult RunConsciousRoutine()
        {
            Topic[] topics = {  new Topic() { name = "Ebola", positive=false},
                                new Topic() { name = "football game", positive=true},
                                new Topic() { name = "weather", positive=true},
                             };

            Random rand = new Random();
            int index = rand.Next(0, 3);

            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
            Interaction i = face.RememberSingleValue("How bout that", topics[index].name);
            face.Talk("Uh huh.");
            return MakeRoutineResult(i);
        }
    }
}
