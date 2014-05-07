using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using RoguePoleDisplay.Screens;
using RoguePoleDisplay.LeapListeners;

namespace RoguePoleDisplay.MenuStates
{
    //public class Chosen : MenuState
    //{
    //    private MenuItem _menuItem;
    //    private double _secondsToDisplay = 7;
    //    private DateTime _finishTime;
    //    private bool _menuWritten = false;
    //    private IScreen _screen;

    //    public Chosen(MenuItem item)
    //    {
    //        if (1 == item.choiceNumber)
    //            _screen = new BasicScreen("Basic screen.", "It is not special.");
    //        else
    //            _screen = new TypingScreen("Typed screen.", "It's kinda fancy.", 200);

    //        _finishTime = DateTime.Now.AddSeconds(_secondsToDisplay);
    //        _menuItem = item;
    //    }

    //    public void OnLeapFrame(PoleDisplay poleDisplay, GameState gameState, Controller controller)
    //    {
    //        if (!_menuWritten)
    //        {
    //            poleDisplay.Clear();
    //            _screen.Draw(poleDisplay, gameState, controller);
    //            _menuWritten = true;
    //        }

    //        if (DateTime.Now > _finishTime)
    //            gameState.currentState = new MenuShow();
    //    }

    //}
}
