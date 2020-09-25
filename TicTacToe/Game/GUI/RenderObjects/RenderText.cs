using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    class RenderText : IRenderObject
    {
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public float ScaleX { get; protected set; }
        public float FontSize { get; protected set; }
        public string Text { get; protected set; }
        protected IRenderable Actor { get; set; }

        public Font Font;

        private Color Color { get; set; }

        public RenderText(Position position, IRenderable actor, String text) : this(position, actor, Color.Black, text) { }

        public RenderText(Position position, IRenderable actor, Color color, String text) : this(position.X, position.Y, position.Width, position.Height, actor, color, text) { }

        public RenderText(IRenderable actor, Color color, String text) : this(0, 0, 0, 0, actor, color, text) { }

        public RenderText(int positionX, int positionY, int width, int fontHeight, IRenderable actor, Color color, string text)
        {
            PositionX = positionX;
            PositionY = positionY;
            Actor = actor;
            Color = color;
            Text = text;
            Font = new Font("assets/fonts/Lato-Regular.ttf");

            if (fontHeight != 0)
            {
                SetFontSize(fontHeight);
            }
            else
            {
                FontSize = 1;
            }
        }


        public void SetFontSize(int height)
        {
            FontSize = (float)height;
        }

        public IRenderable GetActor()
        {
            return Actor;
        }

        public Transformable GetShape()
        {
            Text text = new Text(Text, Font, (uint)FontSize)
            {
                Position = new Vector2f(PositionX, PositionY),
                FillColor = Color,
            };
            return text;
        }

        public bool IsPointInside(int x, int y)
        {
            return false;
        }

        public void SetSize(int positionX, int positionY, int width, int height)
        {
            PositionX = positionX;
            PositionY = positionY;
            SetFontSize(height);
        }
    }
}
