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

        public Interaction SlowTalk(string text, int msTypingDelay = 200, int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.SlowType(text, msTypingDelay);
            result.displayText = text;
            Thread.Sleep(millisecondTimeout);
            return result;
        }

        public Interaction Talk(string text, int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.Write(text);
            result.displayText = text;
            Thread.Sleep(millisecondTimeout);
            return result;
        }

        public Interaction RememberSingleValue(string text, bool longTerm = false, int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.Write(text);
            result.displayText = text;
            int intResult;
            if (_input.TryGetInteger(out intResult, millisecondTimeout))
            {
                result.resultValue = intResult;
            }
            
            if(result.resultValue != -1)
                Memory.GetInstance().AddToMemory(result, longTerm);
            
            return result;

        }

        public Interaction GetSingleValue(string text, int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.Write(text);
            result.displayText = text;
            int intResult;
            if (_input.TryGetInteger(out intResult, millisecondTimeout))
            {
                result.resultValue = intResult;
            }

            return result;
        }

        public Interaction YesNo(string text, bool longTerm = false, int millisecondTimeout = 10000)
        {
            Menu menu = new MenuYesNo();
            Interaction result = new Interaction();
            _renderer.Clear();
            _renderer.Write(text);
            _renderer.DisplayMenu(menu);
            result.displayText = text;
            MenuItem item = _input.ChooseFromMenu(menu, millisecondTimeout);
            result.resultValue = item.choiceNumber;

            if (result.resultValue != -1)
                Memory.GetInstance().AddToMemory(result, longTerm);

            return result;
        }

        public Interaction Remembrance(string textTemplate, bool longTerm = true, int millisecondTimeout = 5000, params string[] memoryKeys)
        {
            List<object> rememberedValues = new List<object>();
            foreach (string s in memoryKeys)
            {
                Interaction i = Memory.GetInstance().Remember(s, longTerm);
                if(null != i)
                    rememberedValues.Add(i.resultValue);
            }

            Interaction result = new Interaction();
            result.displayText = string.Format(textTemplate, rememberedValues.ToArray());
            _renderer.Clear();
            _renderer.Write(result.displayText);
            Thread.Sleep(millisecondTimeout);
            return result;
        }

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
