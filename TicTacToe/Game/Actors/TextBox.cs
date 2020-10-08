using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    internal class TextBox : Actor, IRenderable, ITextContainer
    {
        private RenderRectangle TextBoxRectangle;
        private AlignedRenderText TextBoxText;
        public int FontSize { get; }

        public TextBox(Position position, Gamestate gamestate, Vector2f margins, int fontSize, TextPosition horizontalPosition, TextPosition verticalPosition, string text) : base(position, gamestate)
        {
            if (text.Length == 0) throw new ArgumentException("Text cannot be empty", "text");

            FontSize = fontSize;

            TextBoxRectangle = new RenderRectangle(new Position(), this);
            TextBoxText = new AlignedRenderText(new Position(), margins, 0, horizontalPosition, verticalPosition, this, Color.Black, text);

            RecalculateComponentsPositions();
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> { TextBoxRectangle, TextBoxText };
        }

        public override void RecalculateComponentsPositions()
        {
            TextBoxRectangle.SetPosition(CalculateScreenSpacePosition(Position));
            TextBoxText.SetPosition(CalculateScreenSpacePosition(Position));
            TextBoxText.SetFontSize(CalculateScreenSpaceHeight(FontSize));
        }
    }
}