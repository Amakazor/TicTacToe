using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    internal class Text : Actor, IRenderable, ITextContainer
    {
        private AlignedRenderText TextText;
        public int FontSize { get; }

        public Text(Position position, Gamestate gamestate, Vector2f margins, int fontSize, TextPosition horizontalPosition, TextPosition verticalPosition, string text) : base(position, gamestate)
        {
            if (text.Length == 0) throw new ArgumentException("Text cannot be empty", "text");

            FontSize = fontSize;

            TextText = new AlignedRenderText(new Position(), margins, 0, horizontalPosition, verticalPosition, this, Color.Black, text);

            RecalculateComponentsPositions();
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> {  TextText };
        }

        public override void RecalculateComponentsPositions()
        {
            TextText.SetPosition(CalculateScreenSpacePosition(Position));
            TextText.SetFontSize(CalculateScreenSpaceHeight(FontSize));
        }
    }
}