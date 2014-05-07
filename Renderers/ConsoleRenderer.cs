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

        public void Write(string text)
        {
            Console.WriteLine(text);
        }

        public void SlowType(string text, int msTypingDelay = 200)
        {
            foreach (char c in text)
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
            Console.Write("Enter Choice: ");
        }
    }
}
