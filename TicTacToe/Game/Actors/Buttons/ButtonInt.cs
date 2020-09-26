using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    class ButtonInt : Actor, IRenderable, IClickable
    {
        private Action<MouseButtonEventArgs, int> Action { get; set; }
        private int Value { get; set; }
        private string Text { get; set; }

        public ButtonInt(Position position, Gamestate gamestate, Action<MouseButtonEventArgs, int> action, int value, string text) : base(position, gamestate)
        {
            Action = action;
            Value = value;
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
            Action.Invoke(args, Value);
        }
    }
}
