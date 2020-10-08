using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal abstract class TextButton : Button, ITextContainer
    {
        public int FontSize { get; }

        private RenderRoundedRectangle ButtonRectangle;
        private AlignedRenderText ButtonText;

        public TextButton(Position position, Gamestate gamestate, Vector2f margins, int fontSize, TextPosition horizontalPosition, TextPosition verticalPosition, string text) : base(position, gamestate)
        {
            if (text.Length == 0) throw new ArgumentException("Text cannot be empty", "text");

            ButtonRectangle = new RenderRoundedRectangle(new Position(), this, Color.White, new Color(120, 160, 255), CalculateScreenSpaceHeight(5), CalculateScreenSpaceHeight(20));
            ButtonText = new AlignedRenderText(new Position(), margins, 0, horizontalPosition, verticalPosition, this, Color.Black, text);
            FontSize = fontSize;
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> { ButtonRectangle, ButtonText };
        }

        public override void RecalculateComponentsPositions()
        {
            ButtonRectangle.SetPosition(CalculateScreenSpacePosition(Position));
            ButtonRectangle.Radius = (CalculateScreenSpaceHeight(20));
            ButtonRectangle.OutlineThickness = (CalculateScreenSpaceHeight(5));

            ButtonText.SetPosition(CalculateScreenSpacePosition(Position));
            ButtonText.SetFontSize(CalculateScreenSpaceHeight(FontSize));
        }
    }
}