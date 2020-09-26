using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    class ScreenChangeButton : Actor, IRenderable, IClickable
    {
        public string Text { get; }
        public EScreens Screen { get; }

        public ScreenChangeButton(Position position, Gamestate gamestate, string text, EScreens screen) : base(position, gamestate)
        {
            Text = text;
            Screen = screen;
        }

        public List<IRenderObject> GetRenderObjects()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>
            {
                new RenderRectangle(Position, this)
            };

            if (Text.Length > 0)
            {
                renderObjects.Add(new RenderText(Position, this, Text));
            }

            return renderObjects;
        }

        public void OnClick(MouseButtonEventArgs args)
        {
            MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = Screen });
        }
    }
}
