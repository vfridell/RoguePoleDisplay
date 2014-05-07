using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Renderers;
using RoguePoleDisplay.Input;

namespace RoguePoleDisplay.Routines
{
    class ChitChat : Routine
    {
        public override void Init()
        {
            routineType = RoutineType.Awake;
        }

        public override Interaction Run()
        {
            var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());

            face.Talk("So happy to be here!");
            face.RememberSingleValue("What's new?");
            face.Talk("Uh huh.");
            return new Interaction();
        }
    }
}
