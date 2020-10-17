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

        protected override void OnStateChange(ButtonState buttonState)
        {
            if (ButtonText != null && ButtonRectangle != null)
            {
                switch (buttonState)
                {
                    case ButtonState.Active:
                        ButtonText.SetColor(Color.Black);
                        ButtonRectangle.SetColor(Color.White);
                        break;

                    case ButtonState.Inactive:
                        ButtonText.SetColor(new Color(125, 125, 125));
                        ButtonRectangle.SetColor(new Color(220, 220, 220));
                        break;

                    case ButtonState.Focused:
                        ButtonText.SetColor(Color.Black);
                        ButtonRectangle.SetColor(new Color(225, 225, 255));
                        break;
                }
            }
        }

        public string GetText()
        {
            return ButtonText.Text;
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