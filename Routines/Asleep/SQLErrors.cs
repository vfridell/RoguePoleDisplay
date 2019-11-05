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
                Random rnd = new Random();
                string[] errStrings = new string[] 
                {
                    SQLErrorData.SQLErrors[rnd.Next(0, SQLErrorData.SQLErrors.Length - 1)],
                    SQLErrorData.SQLErrors[rnd.Next(0, SQLErrorData.SQLErrors.Length - 1)],
                    SQLErrorData.SQLErrors[rnd.Next(0, SQLErrorData.SQLErrors.Length - 1)],
                    SQLErrorData.SQLErrors[rnd.Next(0, SQLErrorData.SQLErrors.Length - 1)],
                    SQLErrorData.SQLErrors[rnd.Next(0, SQLErrorData.SQLErrors.Length - 1)],
                };
                StringBuilder topLine = new StringBuilder();
                StringBuilder bottomLine = new StringBuilder();
                foreach (string err in errStrings)
                {
                    string[] value = err.Split('*');
                    topLine.Append(value[0].PadRight(20));
                    bottomLine.Append(value[1] + " --- ");
                }
                i = face.RememberSingleValueWithScroll(memory, topLine.ToString(), bottomLine.ToString());
                return MakeRoutineResult(memory, i);
            }
        }
    }
}
