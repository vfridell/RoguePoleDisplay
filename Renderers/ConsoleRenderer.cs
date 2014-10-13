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
        }

        public void Write(string line1, string line2)
        {
            Console.WriteLine(line1);
            Console.WriteLine(line2);
        }

        public void SlowType(string line1, string line2, int msTypingDelay = 200)
        {
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
            Console.WriteLine();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void DisplayMenu(Menu menu)
        {
            List<MenuItem> items = menu.GetMenuItems();
            items.ForEach((i) => Console.WriteLine(i.choiceNumberAndText));
        }

        public void WritePosition(char c, int x, int y)
        {
            throw new NotImplementedException();
        }

        #region IScreenRenderer Members


        public void WriteProgressIndicator(int total, int start, int current)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
