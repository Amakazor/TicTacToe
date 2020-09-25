using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    class Button : Actor, IRenderable, IClickable
    {
        private Action<MouseButtonEventArgs> Action;
        private string Text;

        public Button(Position position, Gamestate gamestate, Action<MouseButtonEventArgs> action, string text) : base(position, gamestate)
        {
            Action = action;
            Text = text;
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
            Action.Invoke(args);
        }
    }
}
