using SFML.Window;
using System;

namespace TicTacToe.Game.Events
{
    class PlayerClickedEventArgs : EventArgs
    {
        public Mouse.Button Button;
        public int X;
        public int Y;
        public int PlayerID;

        public PlayerClickedEventArgs(MouseButtonEventArgs mouseButtonEventArgs, int playerID) : this(mouseButtonEventArgs.Button, mouseButtonEventArgs.X, mouseButtonEventArgs.Y, playerID) { }
        public PlayerClickedEventArgs(Mouse.Button button, int x, int y, int playerId)
        {
            this.Button = button;
            this.X = x;
            this.Y = y;
            this.PlayerID = playerId;
        }
    }
}
