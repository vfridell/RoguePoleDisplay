using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoguePoleDisplay
{
    public abstract class Menu 
    {
        public abstract int NumberOfChoices { get; }
        public abstract bool ValidChoice(int choice);

        protected string _topLine;
        public string topLine 
        {
            get
            {
                return _topLine;
            }
            set
            {
                _topLine = value.PadRight(20).Substring(0, 20);
            }
        }

        protected Dictionary<int, MenuItem> _menuItems = new Dictionary<int, MenuItem>();

        public Menu()
        {
        }

        public virtual void Add(string itemText, int choice)
        {
            if (!ValidChoice(choice)) throw new ArgumentOutOfRangeException("choice", string.Format("Must be between 1 and {0}", NumberOfChoices));
            _menuItems[choice] = new MenuItem() { choiceNumber = choice, text = itemText };
        }

        public virtual MenuItem GetMenuItem(int choice)
        {
            if (!ValidChoice(choice)) throw new ArgumentOutOfRangeException("choice", string.Format("Must be between 1 and {0}", NumberOfChoices));
            return _menuItems[choice];
        }

        public virtual List<MenuItem> GetMenuItems()
        {
            return _menuItems.Values.ToList<MenuItem>();
        }

        public virtual void Highlight(int choice)
        {
            _menuItems.Values.ToList().ForEach((i) => i.highlight = false);
            if (ValidChoice(choice))
            {
                _menuItems[choice].highlight = true;
            }
        }
    }

    public class MenuYesNo : Menu
    {
        public MenuYesNo(string pTopLine)
        {
            topLine = pTopLine;
            _menuItems[1] = new MenuItem() { choiceNumber = 1, text = "Yes" };
            _menuItems[2] = new MenuItem() { choiceNumber = 2, text = "No" };
        }

        public override int NumberOfChoices { get { return 2; } }
        public override bool ValidChoice(int choice) { return choice == 2 || choice == 1; }
    }

    public class MenuTwo : Menu
    {
        public MenuTwo(string pTopLine, string choice1, string choice2)
        {
            topLine = pTopLine;
            _menuItems[1] = new MenuItem() { choiceNumber = 1, text = choice1 };
            _menuItems[2] = new MenuItem() { choiceNumber = 2, text = choice2 };
        }
        public override int NumberOfChoices { get { return 2; } }
        public override bool ValidChoice(int choice) { return choice == 2 || choice == 1; }
    }

    public class MenuFour : Menu
    {
        public MenuFour(string choice1, string choice2, string choice3, string choice4)
        {
            _menuItems[1] = new MenuItem() { choiceNumber = 1, text = choice1 };
            _menuItems[2] = new MenuItem() { choiceNumber = 2, text = choice2 };
            _menuItems[3] = new MenuItem() { choiceNumber = 2, text = choice3 };
            _menuItems[4] = new MenuItem() { choiceNumber = 2, text = choice4 };
        }

        public override int NumberOfChoices { get { return 4; } }
        public override bool ValidChoice(int choice) { return choice <= 4 && choice >= 1; }
    }
}
