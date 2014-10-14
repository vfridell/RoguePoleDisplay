using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace RoguePoleDisplay
{
    public class PoleDisplay : IDisposable
    {
        private Object myLock = new Object();

        private PoleDisplay() 
        {
        }

        private bool _initialized = false;
        public void Initialize()
        {
            if (!_initialized)
            {
                _serialPort = new SerialPort("COM3", 4800, Parity.None, 8, StopBits.One);
                _serialPort.Open();
                _initialized = true;
                Clear();
                SetNoVerticalScroll();
            }
        }

        private static PoleDisplay _poleDisplay;
        public static PoleDisplay GetInstance()
        {
            if (null == _poleDisplay)
            {
                _poleDisplay = new PoleDisplay();
            }
            return _poleDisplay;
        }

        public void RPS10_Power_switch()
        {
            byte [] hammerTheEnterKey = {0x0d,0x0d};
            byte [] togglePowerCommand = {0x02,0x18,0x18,0x02,0x18,0x18,0x30,0x54,0x0d};
            byte [] readbuffer = new byte[1024];
            int bytesRead;
//            bytesRead = _serialPort.Read(readbuffer, 0, 1024);
            _serialPort.Write(hammerTheEnterKey, 0, hammerTheEnterKey.Length);
            System.Threading.Thread.Sleep(1000);
            _serialPort.Write(togglePowerCommand, 0, togglePowerCommand.Length);
            System.Threading.Thread.Sleep(1000);
            bytesRead = _serialPort.Read(readbuffer, 0, 1024);
        }

        public enum DimLevel { LOWEST = '\x20', LOW = '\x40', HIGH = '\x60', HIGHEST = '\xFF' }
        public enum ControlCommand 
        { 
            DIM = '\x04',
            BACKSPACE = '\x08',
            TAB = '\x09',
            LINEFEED = '\x0A',
            RETURN = '\x0D',
            SETPOSITION = '\x10',
            NORMALDISPLAY = '\x11',
            VERTICALSCROLL = '\x12',
            RESET = '\x1F',
            FLASHSTART = '\x1C',
            FLASHSTOP = '\x1D',
            CLEAREOL = '\x18',
            CLEAREOD = '\x19',
            CLEARALL = '\x1E',
            DISABLEMENUSWITCHES = '\x15'
        }

        private SerialPort _serialPort;

        public void Clear()
        {
            Write(ControlCommand.CLEARALL);
        }

        public void Reset()
        {
            Write(ControlCommand.RESET);
        }

        public DimLevel CurrentDimLevel
        {
            set
            {
                Write(ControlCommand.DIM);
                Write(((char)value).ToString());
            }
        }

        public void SetNoVerticalScroll()
        {
            Write(ControlCommand.NORMALDISPLAY);
        }

        public void FadeIn()
        {
            Fade('#');
        }

        public void FadeOut()
        {
            Fade(' ');
        }

        public void Fade(char c, int millisecondsBetweenSteps = 0)
        {
            List<char> cPositions = new List<char>();
            for (int i = 0; i < 40; i++)
            {
                cPositions.Add((char)i);
            }

            Random rnd = new Random();
            for (int i = 39; i >= 0; i--)
            {
                int pos = rnd.Next(0, i);
                char cpos = cPositions[pos];
                Write(ControlCommand.SETPOSITION);
                Write(cpos.ToString());
                Write(c.ToString());
                cPositions.Remove(cpos);
                if (millisecondsBetweenSteps > 0) Thread.Sleep(millisecondsBetweenSteps);
            }
        }

        public void WritePos(char c, int x, int y)
        {
            int pos = x + (y * 20);
            if(pos > 39) throw new Exception(string.Format("Bad position given: {0}, {1}", x, y));

            char cpos = (char)pos;
            Write(ControlCommand.SETPOSITION);
            Write(cpos.ToString());
            Write(c.ToString());
        }

        public void WriteMenu(Menu menu)
        {
            if (menu.NumberOfChoices == 2)
            {
                Write(menu.topLine);
                Write(menu.GetMenuItem(1).choiceNumberAndText);
                Write('|');
                Write(menu.GetMenuItem(2).choiceNumberAndText);
            }
            else if(menu.NumberOfChoices == 4)
            {
                Write(menu.GetMenuItem(1).choiceNumberAndText);
                Write('|');
                Write(menu.GetMenuItem(2).choiceNumberAndText);
                Write(menu.GetMenuItem(3).choiceNumberAndText);
                Write('|');
                Write(menu.GetMenuItem(4).choiceNumberAndText);
            }
            else
            {
                throw new Exception(string.Format("Strange menu type detected."));
            }
        }

        public void Write(string text)
        {
            lock (myLock)
            {
                _serialPort.Write(text);
            }
        }

        public void Write(ControlCommand cmd)
        {
            lock (myLock)
            {
                _serialPort.Write(((char)cmd).ToString());
            }
        }

        public void Write(char cmd)
        {
            lock (myLock)
            {
                _serialPort.Write(cmd.ToString());
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_initialized)
            {
                _serialPort.Close();
                _serialPort.Dispose();
                _initialized = false;
            }
        }

        #endregion
    }
}
