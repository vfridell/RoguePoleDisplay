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
            Console.Write("Enter choice: ");
            string val = Reader.ReadLine(millisecondTimeout);
            if (!int.TryParse(val, out choice) || !menu.ValidChoice(choice))
                return menu.GetMenuItem(choice);
            else
                return null;
        }
    }

    /// <summary>
    /// Uses a thread to allow for a timeout on Console.Readline
    /// </summary>
    class Reader
    {
        private static Thread _inputThread;
        private static AutoResetEvent _getInput, _gotInput;
        private static string _input;

        static Reader()
        {
            _inputThread = new Thread(reader);
            _inputThread.IsBackground = true;
            _getInput = new AutoResetEvent(false);
            _gotInput = new AutoResetEvent(false);
        }

        public static void Start()
        {
            _inputThread.Start();
        }

        private static void reader()
        {
            try
            {
                while (true)
                {
                    _getInput.WaitOne();
                    _input = Console.ReadLine();
                    _gotInput.Set();
                }
            }
            catch (Exception ex)
            {
                // TODO how to handle?
            }
        }

        public static string ReadLine(int timeOutMillisecs)
        {
            _getInput.Set();
            bool success = _gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return _input;
            else
                return "";
        }
    }
}
