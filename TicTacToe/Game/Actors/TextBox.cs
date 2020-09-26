using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    class TextBox : Actor, IRenderable
    {
        private string Text;
        private Position RelativeTextPosition;

        public TextBox(Position position, Position relativeTextPosition, Gamestate gamestate, string text) : base(position, gamestate)
        {
            RelativeTextPosition = relativeTextPosition;
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
                renderObjects.Add(new RenderText(CalculateTextPosition(), this, Text));
            }

            return renderObjects;
        }

        private Position CalculateTextPosition()
        {
            return new Position(Position.X + RelativeTextPosition.X, Position.Y + RelativeTextPosition.Y, 0, RelativeTextPosition.Height);
        }
    }
}
