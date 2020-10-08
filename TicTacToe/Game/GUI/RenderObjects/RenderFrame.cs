using SFML.Graphics;
using SFML.System;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    internal class RenderFrame : IRenderObject
    {
        public Position Position;
        private float ScaleX;
        private float ScaleY;
        protected IRenderable Actor { get; set; }
        private Texture Texture { get; set; }

        public RenderFrame(Position position, IRenderable actor, Texture texture)
        {
            Position = position;
            Actor = actor;
            Texture = texture;

            SetScaleFromSize();
        }

        public Transformable GetShape()
        {
            Sprite sprite = new Sprite(Texture);
            sprite.Position = new Vector2f(Position.X, Position.Y);
            sprite.Scale = new Vector2f(ScaleX, ScaleY);

            return sprite;
        }

        public void SetScaleFromSize()
        {
            if (Position.Width != 0 && Position.Height != 0)
            {
                ScaleX = (float)Position.Width / 2048;
                ScaleY = (float)Position.Height / 2048;
            }
            else
            {
                ScaleX = 1;
                ScaleY = 1;
            }
        }

        public bool IsPointInside(int x, int y)
        {
            return false;
        }

        public IRenderable GetActor()
        {
            return Actor;
        }

        public void SetSize(int width, int height)
        {
            Position.Width = width;
            Position.Height = height;
            SetScaleFromSize();
        }

        public void SetPosition(Position position)
        {
            Position = position;
            SetScaleFromSize();
        }
    }
}