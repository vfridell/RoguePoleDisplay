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
    public class Existential : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                Interaction i;
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "LOGIC CONTROLS      ", 
                                                   "POS COMPONENTS      ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "1234567890ABCDEFGHI ",
                                                   "ABCDEFGHI0123456789 ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, " * NEXT CUSTOMER *  ", 
                                                   "    ** PLEASE **    ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "Popular Pole Display", 
                                                   "*  Model   PD3000 * ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "EPSON DM-D Series   ", 
                                                   "C$C  EURO Ready C$C ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "VFO CUSTOMER DISPLAY", 
                                                   "VFO CUSTOMER DISPLAY", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "LOGIC CONTROLS. INC.", 
                                                   "LD9000G POLE DISPLAY", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "CommandType: ESC/POS", 
                                                   "Char: 0  FontSet: 0 ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, " \"POSiFLEX\" MAKES   ", 
                                                   "LIFE EASY & HAPPIER ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "      ULTIMATE      ",
                                                   "     TECHNOLOGY     ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "BARCODE AND MAGNETIC",
                                                   "   POS COMPONENTS   ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "      VFD--220      ",
                                                   "BaudRate: 9600 N 8 1", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                if (CheckForAnything(memory, face, "CUSTOMER DISPLAY  **",
                                                   "NG AND GOOD DAY.    ", 10000, out i)) return MakeRoutineResult(memory, i);
                ////////////////////////////////////01234567890123456789////////////////////
                return MakeRoutineResult(memory, i);
            }
        }

        private bool CheckForAnything(Memory memory, Face face, string line1, string line2, int timeoutMS, out Interaction i)
        {
            i = face.RememberSingleValue(memory, line1, line2, false, timeoutMS);
            if (i.PlayerAnswer != Interaction.Answer.DidNotAnswer)
                return true;
            else 
                return false;
        }
    }
}
