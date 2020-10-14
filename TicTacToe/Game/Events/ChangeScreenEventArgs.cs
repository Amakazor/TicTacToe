using System;

namespace TicTacToe.Game.Events
{
    internal class ChangeScreenEventArgs : EventArgs
    {
        public ScreenType Screen;
        public bool ChangePreviousScreen = true;
    }
}