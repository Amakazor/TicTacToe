using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    class TextBox : Actor, IRenderable, ITextContainer
    {
        protected string Text;
        protected Position RelativeTextPosition;

        public TextBox(Position position, Position relativeTextPosition, Gamestate gamestate, string text) : base(position, gamestate)
        {
            RelativeTextPosition = relativeTextPosition;
            Text = text;
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>
            {
                new RenderRectangle(CalculateScreenSpacePosition(Position), this)
            };

            if (Text.Length > 0)
            {
                renderObjects.Add(new RenderText(CalculateScreenSpacePosition(((ITextContainer)this).CalculateTextPosition(Position, RelativeTextPosition)), this, Text));
            }

            return renderObjects;
        }
    }
}
