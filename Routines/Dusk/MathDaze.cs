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
    [RoutineType(RoutineType.Dusk)]
    public class MathDaze : Routine
    {
        protected override RoutineResult RunConsciousRoutine()
        {
            using (var memory = new Memory())
            {
                var face = new Face(RendererFactory.GetPreferredRenderer(), InputFactory.GetPreferredInput());
                face.Talk(memory, "Hey...");
                face.Talk(memory, "I've got a", " question for you");
                Interaction i;
                int correctAnswers = 0;
                bool correct = false;
                do
                {
                    MathProblem problem = new MathProblem();
                    i = face.GetSingleValue(memory, problem.ToString());
                    if (i.ResultValue == problem.TheAnswer)
                    {
                        correctAnswers++;
                        correct = true;
                        face.Talk(memory, "Yes!");
                        if (correctAnswers == 1)
                        {
                            face.Talk(memory, "You can help me", " with something else");
                        }
                        else
                        {
                            face.Talk(memory, "Nice.", string.Format("That's {0} in a row", correctAnswers));
                        }
                    }
                    else if (i.PlayerAnswer == Interaction.Answer.DidNotAnswer)
                    {
                        correct = false;
                        face.SlowTalk(memory, "Stumped??");
                        face.Talk(memory, "You should probably", " go to class");
                    }
                    else
                    {
                        correct = false;
                        face.Talk(memory, "Good guess");
                        face.Talk(memory, "", " but no.");
                    }
                } while (correct);
                return MakeRoutineResult(memory, i);
            }
        }
    }

    class MathProblem
    {
        public MathProblem()
        {
            Random rnd = new Random();
            theOperator = (Operators)rnd.Next(0, 4);
            switch (theOperator)
            {
                case Operators.times:
                    value1 = rnd.Next(1, 4);
                    value2 = rnd.Next(1, 3);
                    break;
                case Operators.plus:
                    value1 = rnd.Next(1, 5);
                    value2 = rnd.Next(0, 5);
                    break;
                case Operators.minus:
                    value1 = rnd.Next(5, 10);
                    value2 = rnd.Next(0, 4);
                    break;
                case Operators.divide:
                    int[] denominator = { 12, 2, 3, 4, 6 };
                    value1 = 12;
                    value2 = denominator[rnd.Next(0, 5)];
                    break;

            }
        }

        public int value1;
        public int value2;
        public Operators theOperator;

        public enum Operators { plus = 0, minus = 1, times = 2, divide = 3 };
        public delegate int MathOperation(int x, int y);
        public MathOperation[] operations = {(x,y) => { return x + y; },
                                             (x,y) => { return x - y; },
                                             (x,y) => { return x * y; },
                                             (x,y) => { return x / y; },
                                            };
        public int TheAnswer
        {
            get
            {
                return operations[(int)theOperator](value1, value2);
            }
        }

        public override string ToString()
        {
            //////////////////////01234567890123456789////////////////////
            return string.Format("   {0} {1} {2} = ?", value1, theOperator, value2);
        }
    }
}
