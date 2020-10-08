using SFML.Graphics;
using SFML.System;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    internal class RenderSymbol : IRenderObject
    {
        public Position Position { get; protected set; }
        public float ScaleX { get; protected set; }
        public float ScaleY { get; protected set; }
        protected IRenderable Actor { get; set; }

        private Texture Texture { get; set; }
        public Color Color { get; set; }

        public RenderSymbol(Position position, IRenderable actor, Texture texture) : this(position, actor, texture, Color.White) { }
        public RenderSymbol(IRenderable actor, Texture texture, Color color) : this(new Position(), actor, texture, color) { }
        public RenderSymbol(Position position, IRenderable actor, Texture texture, Color color)
        {
            Position = position;
            Actor = actor;
            Texture = texture;
            Color = color;

            SetScaleFromSize();
        }

        public Transformable GetShape()
        {
            Sprite sprite = new Sprite(Texture);
            sprite.Position = new Vector2f(Position.X, Position.Y);
            sprite.Scale = new Vector2f(ScaleX, ScaleY);
            sprite.Color = Color;

            return sprite;
        }

        public void SetScaleFromSize()
        {
            if (Position.Width != 0 && Position.Height != 0)
            {
                ScaleX = (float)Position.Width / 256;
                ScaleY = (float)Position.Height / 256;
            }
            else
            {
                ScaleX = 1;
                ScaleY = 1;
            }
        }

        public bool IsPointInside(int x, int y)
        {
            return (Position.X <= x) && (Position.X + Position.Width >= x) && (Position.Y <= y) && (Position.Y + Position.Height >= y);
        }

        public void SetSize(int width, int height)
        {
            Position = new Position(Position.X, Position.Y, width, height);
            SetScaleFromSize();
        }

        public void SetPosition(Position position)
        {
            Position = position;
            SetScaleFromSize();
        }

        public IRenderable GetActor()
        {
            return Actor;
        }
    }
}