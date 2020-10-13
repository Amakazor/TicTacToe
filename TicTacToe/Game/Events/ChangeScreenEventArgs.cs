using System;

namespace TicTacToe.Game.Events
{
    class ChangeScreenEventArgs : EventArgs
    {
        public ScreenType Screen;
        public bool ChangePreviousScreen = true;
    }
}
