using SFML.System;
using SFML.Window;
using System;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal class ChangePageButton : TextButton
    {
        private Action<MouseButtonEventArgs, bool> Action;
        private readonly bool changeToNext;

        public ChangePageButton(Position position, Gamestate gamestate, Vector2f margins, int fontSize, TextPosition horizontalPosition, TextPosition verticalPosition, string text, Action<MouseButtonEventArgs, bool> action, bool changeToNext) : base(position, gamestate, margins, fontSize, horizontalPosition, verticalPosition, text)
        {
            Action = action;
            this.changeToNext = changeToNext;
        }

        public override bool OnClick(MouseButtonEventArgs args)
        {
            if (base.OnClick(args))
            {
                Action.Invoke(args, changeToNext);
                return true;
            }

            return false;
        }
    }
}