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
        protected Position Position;
        public string Text { get; protected set; }
        protected IRenderable Actor { get; set; }

        public Font Font;

        private Color Color { get; set; }

        public RenderText(Position position, IRenderable actor, string text) : this(position, actor, Color.Black, text) { }
        public RenderText(IRenderable actor, Color color, string text) : this(new Position(), actor, color, text) { }
        public RenderText(Position position, IRenderable actor, Color color, string text)
        {
            Position = position;
            Actor = actor;
            Color = color;
            Text = text;
            Font = new Font("assets/fonts/Lato-Regular.ttf");

            if (Position.Height <= 1)
            {
                Position.Height = 1;
            }
        }


        public void SetFontSize(int height)
        {
            
        }

        public IRenderable GetActor()
        {
            return Actor;
        }

        public Transformable GetShape()
        {
            Text text = new Text(Text, Font, (uint)Position.Height)
            {
                Position = new Vector2f(Position.X, Position.Y),
                FillColor = Color,
            };
            return text;
        }

        public bool IsPointInside(int x, int y)
        {
            return false;
        }

        public void SetSize(int width, int height)
        {
            SetFontSize(height);
        }

        public void SetPosition(Position position)
        {
            Position = position;
        }
    }
}
