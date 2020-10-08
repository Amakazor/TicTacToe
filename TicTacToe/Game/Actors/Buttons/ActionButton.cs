using SFML.System;
using SFML.Window;
using System;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal class ActionButton : TextButton
    {
        private Action<MouseButtonEventArgs> Action;

        public ActionButton(Position position, Gamestate gamestate, Vector2f margins, int fontSize, TextPosition horizontalPosition, TextPosition verticalPosition, string text, Action<MouseButtonEventArgs> action) : base(position, gamestate, margins, fontSize, horizontalPosition, verticalPosition, text)
        {
            Action = action;
        }

        public override void OnClick(MouseButtonEventArgs args)
        {
            Action.Invoke(args);
        }
    }
}