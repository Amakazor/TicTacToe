using System;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    class ActionButton : Button
    {
        private Action<MouseButtonEventArgs> Action;

        public ActionButton(Position position, Position relativeTextPosition, Gamestate gamestate, string text, Action<MouseButtonEventArgs> action) : base(position, relativeTextPosition, gamestate, text)
        {
            Action = action;
        }

        public override void OnClick(MouseButtonEventArgs args)
        {
            Action.Invoke(args);
        }
    }
}
