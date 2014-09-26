using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RoguePoleDisplay.Input;
using RoguePoleDisplay.Renderers;
using RoguePoleDisplay.Utilities;
using RoguePoleDisplay.Repositories;

namespace RoguePoleDisplay
{
    public class Face
    {
        IScreenRenderer _renderer;
        IGetInput _input;

        private IndexIncrement _incrementer;

        public Face(IScreenRenderer renderer, IGetInput input)
        {
            _renderer = renderer;
            _input = input;
        }

        public Interaction SlowTalk(string line1, string line2 = "", int msTypingDelay = 200, int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.SlowType(line1, msTypingDelay);
            _renderer.SlowType(line2, msTypingDelay);
            result.displayText = (line1 + " " + line2).Trim();
            Thread.Sleep(millisecondTimeout);
            return result;
        }

        public Interaction Talk(string line1, string line2 = "", int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.Write(line1);
            _renderer.Write(line2);
            result.displayText = (line1 + " " + line2).Trim();
            Thread.Sleep(millisecondTimeout);
            return result;
        }

        public Interaction RememberSingleValue(string line1, string line2 = "", bool longTerm = false, int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.Write(line1);
            _renderer.Write(line2);
            result.displayText = (line1 + " " + line2).Trim();
            int intResult;
            if (_input.TryGetInteger(out intResult, millisecondTimeout))
            {
                result.resultValue = intResult;
            }
            
            if(result.resultValue != -1)
                Memory.GetInstance().AddToMemory(result, longTerm);
            
            return result;

        }

        public Interaction GetSingleValue(string line1, string line2 = "", int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.Write(line1);
            _renderer.Write(line2);
            result.displayText = (line1 + " " + line2).Trim();
            int intResult;
            if (_input.TryGetInteger(out intResult, millisecondTimeout))
            {
                result.resultValue = intResult;
            }

            return result;
        }

        public Interaction YesNo(string line1, bool longTerm = false, int millisecondTimeout = 10000)
        {
            Menu menu = new MenuYesNo();
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.Write(line1);
            _renderer.DisplayMenu(menu);
            result.displayText = line1;
            MenuItem item = _input.ChooseFromMenu(menu, millisecondTimeout);
            if(null != item) result.resultValue = item.choiceNumber;

            if (result.resultValue != -1)
                Memory.GetInstance().AddToMemory(result, longTerm);

            return result;
        }

        //public Interaction Remembrance(string line1, string line2 = "", bool longTerm = true, int millisecondTimeout = 5000, params string[] memoryKeys)
        //{
        //    List<object> rememberedValues = new List<object>();
        //    foreach (string s in memoryKeys)
        //    {
        //        Interaction i = Memory.GetInstance().Remember(s, longTerm);
        //        if(null != i)
        //            rememberedValues.Add(i.resultValue);
        //    }

        //    Interaction result = new Interaction();
        //    _renderer.Clear();
        //    string line1Formatted = string.Format(line1, rememberedValues.ToArray());
        //    string line2Formatted = string.Format(line2, rememberedValues.ToArray());
        //    _renderer.Write(line1Formatted);
        //    _renderer.Write(line2Formatted);
        //    result.displayText = (line1Formatted + " " + line2Formatted).Trim();
        //    Thread.Sleep(millisecondTimeout);
        //    return result;
        //}

        public void ResetIncrementer()
        {
            _incrementer = null;
        }

        public Interaction TalkInCircles(int millisecondTimeout = 5000, params string[] textLines)
        {
            if (null == _incrementer) _incrementer = new IndexIncrement(textLines.Length, true);
            string text = textLines[_incrementer.Next];
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.Write(text);
            result.displayText = text;
            Thread.Sleep(millisecondTimeout);
            return result; ;
        }
    }

}
