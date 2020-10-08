using SFML.System;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal class ScreenChangeButton : TextButton, IRenderable, IClickable
    {
        public ScreenType Screen { get; }

        public ScreenChangeButton(Position position, Gamestate gamestate, Vector2f margins, int fontSize, TextPosition horizontalPosition, TextPosition verticalPosition, string text, ScreenType screen) : base(position, gamestate, margins, fontSize, horizontalPosition, verticalPosition, text)
        {
            Screen = screen;
        }

        public override void OnClick(MouseButtonEventArgs args)
        {
            MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = Screen });
        }
    }
}