﻿using System;
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
    class Nouns : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var nouns = new List<string>() { "Sheep", "Trees", "Birds", "Cars", "Pillows", "Leaves", "PB&J", "Books", "Cookie", "Monsters", "Whaaa?!" };

                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                Interaction i = new Interaction();
                foreach (string noun in nouns)
                {
                    if (CheckForAnything(memory, face, noun, "", out i)) return MakeRoutineResult(memory, i);
                }
                return MakeRoutineResult(memory, i);
            }
        }

        private bool CheckForAnything(Memory memory, Face face, string line1, string line2, out Interaction i)
        {
            i = face.RememberSingleValue(memory, line1, line2, false, 1000);
            if (i.playerAnswer != Interaction.Answer.DidNotAnswer)
                return true;
            else 
                return false;
        }
    }
}
