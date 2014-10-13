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
    class Restaurant : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                Interaction i;
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "1 Hamburger T1  4.95",
                                                   "TAX 0.32 SUBT   5.27", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "Mineral Water       ",
                                                   "               $3.00", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "  TO GO         0.00",
                                                   "TOTAL          13.05", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "   Steak w/ Sushi   ",
                                                   "       $59.99       ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, " Tot 15.53 Pd 15.53 ",
                                                   " Visa - Change .00  ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "ALL YOUR BASE ARE   ",
                                                   "BELONG TO US        ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "1 PIZZA SLICE       ",
                                                   "               $3.75", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "Overpriced Coffee   ",
                                                   "              $37.00", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "Ham Egg and Cheese  ",
                                                   "                5.00", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "Spam, Spam, Spam,   ",
                                                   " Egg & Spam    11.99", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "Kid Dipped Cone     ",
                                                   "               $0.72", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "HORSE RADISH        ",
                                                   "               $1.00", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "Bud Bottle          ",
                                                   "               $4.00", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "Bacon               ",
                                                   "                1.45", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, ">Extra Cheese $0.25 ",
                                                   "                    ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "3 Egg Roll @ 0.56   ",
                                                   "               $1.68", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
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
