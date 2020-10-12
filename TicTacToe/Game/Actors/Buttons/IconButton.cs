using SFML.Graphics;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal abstract class IconButton : Button
    {
        public Texture IconTexture { get; protected set; }
        public float Scale { get; }
        protected RenderSymbol Icon { get; set; }
        protected RenderRoundedRectangle Background { get; set; }

        public IconButton(Position position, Gamestate gamestate, Texture iconTexture, float scale) : base(position, gamestate)
        {
            IconTexture = iconTexture;
            Scale = scale;

            Background = new RenderRoundedRectangle(new Position(), this, Color.White, new Color(120, 160, 255), CalculateScreenSpaceHeight(5), CalculateScreenSpaceHeight(20));
            Icon = new RenderSymbol(new Position(), this, iconTexture);

            RecalculateComponentsPositions();
        }

        public IconButton(Position position, Gamestate gamestate, Texture iconTexture, float scale, Color iconColor) : base(position, gamestate)
        {
            IconTexture = iconTexture;
            Scale = scale;

            Background = new RenderRoundedRectangle(new Position(), this, Color.White, new Color(120, 160, 255), CalculateScreenSpaceHeight(5), CalculateScreenSpaceHeight(20));
            Icon = new RenderSymbol(new Position(), this, iconTexture, iconColor);

            RecalculateComponentsPositions();
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> { Background, Icon };
        }

        public void ChangeColor(Color newColor)
        {
            Icon.Color = newColor;
        }

        protected override void OnStateChange(ButtonState buttonState)
        {
            if (Background != null)
            {
                switch (buttonState)
                {
                    case ButtonState.Active:
                        Background.SetColor(Color.White);
                        break;

                    case ButtonState.Inactive:
                        Background.SetColor(Color.White);
                        break;

                    case ButtonState.Focused:
                        Background.SetColor(new Color(200, 240, 200));
                        break;
                }
            }
        }

        public override void RecalculateComponentsPositions()
        {
            Background.SetPosition(CalculateScreenSpacePosition(Position));
            Background.Radius = (CalculateScreenSpaceHeight(20));
            Background.OutlineThickness = (CalculateScreenSpaceHeight(5));

            Position iconPosition = new Position((int)(Position.X + Position.Width * (1 - Scale) * 0.5), (int)(Position.Y + Position.Height * (1 - Scale) * 0.5), (int)(Position.Width * Scale), (int)(Position.Height * Scale));
            Icon.SetPosition(CalculateScreenSpacePosition(iconPosition));
        }
    }
}