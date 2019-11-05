using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Renderers
{
    public class ConsoleRenderer : IScreenRenderer
    {
        public void Init()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetWindowSize(20, 3);
            Console.Title = "Rogue";
            Console.SetCursorPosition(0, 0);
        }

        public void Write(string line1, string line2)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(line1);
            Console.Write(line2);
        }

        public void SlowType(string line1, string line2, int msTypingDelay = 200)
        {
            Console.SetCursorPosition(0, 0);
            foreach (char c in line1)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(msTypingDelay);
            }
            Console.WriteLine();
            foreach (char c in line2)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(msTypingDelay);
            }
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void DisplayMenu(Menu menu)
        {
            Console.SetCursorPosition(0, 0);
            if (menu.NumberOfChoices == 2)
            {
                Console.Write(menu.topLine);
                Console.WriteLine();
                Console.Write(menu.GetMenuItem(1).choiceNumberAndText);
                Console.Write('|');
                Console.Write(menu.GetMenuItem(2).choiceNumberAndText);
            }
            else if (menu.NumberOfChoices == 4)
            {
                Console.Write(menu.GetMenuItem(1).choiceNumberAndText);
                Console.Write('|');
                Console.Write(menu.GetMenuItem(2).choiceNumberAndText);
                Console.WriteLine();
                Console.Write(menu.GetMenuItem(3).choiceNumberAndText);
                Console.Write('|');
                Console.Write(menu.GetMenuItem(4).choiceNumberAndText);
            }
            else
            {
                throw new Exception(string.Format("Strange menu type detected."));
            }
        }

        public void WritePosition(char c, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        public void WriteProgressIndicator(int total, int start, int current)
        {
            char[] indicators = { '|', '/', '-', '\\', '|', '+' };
            int index = Convert.ToInt32((Convert.ToDouble(current) / Convert.ToDouble(total)) * Convert.ToDouble(total));
            WritePosition(indicators[index], 19, 0);
        }

        public void Fade(char c, int millisecondsBetweenSteps = 0)
        {
            List<(int, int)> cPositions = new List<(int, int)>();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    cPositions.Add((i, j));
                }
            }

            Random rnd = new Random();
            for (int i = 39; i >= 0; i--)
            {
                int index = rnd.Next(0, i);
                (int x, int y) = cPositions[index];

                WritePosition(c, x, y);
                cPositions.Remove((x,y));
                if (millisecondsBetweenSteps > 0) Task.Delay(millisecondsBetweenSteps);
            }
        }

        public void Scroll(string line1, string line2, int msScrollDelay, int numLoops)
        {
            int onePos = 0;
            int twoPos = 0;
            for(int i=0; i<numLoops; i++)
            {
                do
                {
                    Write(line1.Substring(onePos++, 20), line2.Substring(twoPos++, 20));
                    Task.Delay(msScrollDelay);
                    if (onePos >= line1.Length) onePos = 0;
                    if (twoPos >= line2.Length) twoPos = 0;
                } while (onePos < line1.Length || twoPos < line2.Length);
            }
        }
    }
}
