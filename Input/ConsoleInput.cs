using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Input
{
    public class ConsoleInput : IGetInput
    {
        public int GetInteger(int millisecondTimeout)
        {
            string val = Reader.ReadLine(millisecondTimeout);
            int intResult;
            if (int.TryParse(val, out intResult))
                return intResult;

            throw new Exception("Could not parse integer from console input");
        }

        public bool TryGetInteger(out int value, int millisecondTimeout)
        {
            string val = Reader.ReadLine(millisecondTimeout);
            return int.TryParse(val, out value);
        }

        public void Init()
        {
            Reader.Start();
        }

        public MenuItem ChooseFromMenu(Menu menu, int millisecondTimeout)
        {
            int choice;
            string val = Reader.ReadLine(millisecondTimeout);
            while (false == int.TryParse(val, out choice) || !menu.ValidChoice(choice))
            {
                Console.WriteLine("Bad choice");
                Console.Write("Enter choice: ");
                val = Console.ReadLine();
            }

            return menu.GetMenuItem(choice);
        }
    }

    /// <summary>
    /// Uses a thread to allow for a timeout on Console.Readline
    /// </summary>
    class Reader
    {
        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static string input;

        static Reader()
        {
            inputThread = new Thread(reader);
            inputThread.IsBackground = true;
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
        }

        public static void Start()
        {
            inputThread.Start();
        }

        private static void reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = Console.ReadLine();
                gotInput.Set();
            }
        }

        public static string ReadLine(int timeOutMillisecs)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return input;
            else
                return "";
        }
    }
}
