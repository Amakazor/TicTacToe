using System.Collections.Generic;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    class ScreenChangeButton : Button, IRenderable, IClickable
    {
        public EScreens Screen { get; }

        public ScreenChangeButton(Position position, Position relativeTextPosition, Gamestate gamestate, string text, EScreens screen) : base(position, relativeTextPosition, gamestate, text)
        {
            Screen = screen;
        }

        public override void OnClick(MouseButtonEventArgs args)
        {
            MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = Screen });
        }
    }
}
