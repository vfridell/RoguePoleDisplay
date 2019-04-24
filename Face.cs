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
using RoguePoleDisplay.Models;

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

        public Interaction Fade(Memory memory, char c, int millisecondsBetweenSteps = 0)
        {
            Interaction result = new Interaction() { Player = memory.GetCurrentPlayer() };
            _renderer.Fade(c, millisecondsBetweenSteps);
            return result;
        } 

        public Interaction SlowTalk(Memory memory, string line1, string line2 = "", int msTypingDelay = 200, int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction() { Player = memory.GetCurrentPlayer() };
            _renderer.Clear();
            _renderer.SlowType(line1, line2, msTypingDelay);
            result.DisplayText = (line1 + " " + line2).Trim();
            Thread.Sleep(millisecondTimeout);
            return result;
        }

        public Interaction Talk(Memory memory, string line1, string line2 = "", int millisecondTimeout = 3000)
        {
            Interaction result = new Interaction() { Player = memory.GetCurrentPlayer() };
            _renderer.Clear();
            _renderer.Write(line1, line2);
            result.DisplayText = (line1 + " " + line2).Trim();

            int oneFifth = millisecondTimeout / 5;
            for (int i = 0; i <= 5; i++)
            {
                _renderer.WriteProgressIndicator(5, 0, i);
                Thread.Sleep(oneFifth);
            }
            return result;
        }

        public Interaction RememberSingleValue(Memory memory, string line1, string line2 = "", bool longTerm = false, int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction() { Player = memory.GetCurrentPlayer() };
            _renderer.Clear();
            _renderer.Write(line1, line2);
            result.DisplayText = (line1 + " " + line2).Trim();
            int intResult;
            if (_input.TryGetInteger(out intResult, millisecondTimeout))
            {
                result.ResultValue = intResult;
            }
            
            if(result.ResultValue != -1)
                memory.AddToMemory(result, longTerm);
            
            return result;

        }

        public Interaction GetSingleValue(Memory memory, string line1, string line2 = "", int millisecondTimeout = 5000)
        {
            Interaction result = new Interaction() { Player = memory.GetCurrentPlayer() };
            _renderer.Clear();
            _renderer.Write(line1, line2);
            result.DisplayText = (line1 + " " + line2).Trim();
            int intResult;
            if (_input.TryGetInteger(out intResult, millisecondTimeout))
            {
                result.ResultValue = intResult;
            }

            return result;
        }

        public Interaction YesNo(Memory memory, string line1, bool longTerm = false, int millisecondTimeout = 10000)
        {
            Menu menu = new MenuYesNo(line1);
            Interaction result = new Interaction() { Player = memory.GetCurrentPlayer() };
            _renderer.Clear();
            _renderer.DisplayMenu(menu);
            result.DisplayText = line1;
            MenuItem item = _input.ChooseFromMenu(menu, millisecondTimeout);
            if(null != item) result.ResultValue = item.choiceNumber;

            if (result.ResultValue != -1)
                memory.AddToMemory(result, longTerm);

            return result;
        }

        public Interaction TwoChoices(Memory memory, string line1, string choice1, string choice2, bool longTerm = false, int millisecondTimeout = 10000)
        {
            Menu menu = new MenuTwo(line1, choice1, choice2);
            Interaction result = new Interaction() { Player = memory.GetCurrentPlayer() };
            _renderer.Clear();
            _renderer.DisplayMenu(menu);
            result.DisplayText = line1;
            MenuItem item = _input.ChooseFromMenu(menu, millisecondTimeout);
            if (null != item) result.ResultValue = item.choiceNumber;

            if (result.ResultValue != -1)
                memory.AddToMemory(result, longTerm);

            if (null != item)
                result.ResultText = item.plainText;

            return result;
        }

        public void ResetIncrementer()
        {
            _incrementer = null;
        }

        public Interaction TalkInCircles(Memory memory, int millisecondTimeout = 3000, params string[] textLines)
        {
            if (null == _incrementer) _incrementer = new IndexIncrement(textLines.Length, true);
            string text = textLines[_incrementer.Next];
            Interaction result = new Interaction() { Player = memory.GetCurrentPlayer() };
            _renderer.Clear();
            _renderer.Write(text, "");
            result.DisplayText = text;
            Thread.Sleep(millisecondTimeout);
            return result; ;
        }
    }

}
