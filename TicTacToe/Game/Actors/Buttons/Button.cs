using System;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    abstract class Button : TextBox, IClickable
    {
        public Button(Position position, Position relativeTextPosition, Gamestate gamestate, string text) : base(position, relativeTextPosition, gamestate, text){}

        public abstract void OnClick(MouseButtonEventArgs args);
    }
}
