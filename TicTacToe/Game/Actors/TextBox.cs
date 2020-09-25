using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    class TextBox : Actor, IRenderable
    {
        private string Text;
        private Position TextPosition;

        public TextBox(Position position, Position textPosition, Gamestate gamestate, string text) : base(position, gamestate)
        {
            TextPosition = textPosition;
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
                renderObjects.Add(new RenderText(TextPosition, this, Text));
            }

            return renderObjects;
        }
    }
}
