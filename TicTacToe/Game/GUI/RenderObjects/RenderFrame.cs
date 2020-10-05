using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    class RenderFrame : IRenderObject
    {
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public float ScaleX { get; protected set; }
        public float ScaleY { get; protected set; }
        protected IRenderable Actor { get; set; }

        private Texture Texture { get; set; }
        private Color Color { get; set; }

        public RenderFrame(Position position, IRenderable actor, Texture texture) : this(position.X, position.Y, position.Width, position.Height, actor, texture) { }

        public RenderFrame(int positionX, int positionY, int width, int height, IRenderable actor, Texture texture)
        {
            PositionX = positionX;
            PositionY = positionY;
            Actor = actor;
            Texture = texture;

            if (width != 0 && height != 0)
            {
                SetScaleFromSize(width, height);
            }
            else
            {
                ScaleX = 1;
                ScaleY = 1;
            }
        }

        public Transformable GetShape()
        {
            Sprite sprite = new Sprite(this.Texture);
            sprite.Position = new Vector2f(this.PositionX, this.PositionY);
            sprite.Scale = new Vector2f(this.ScaleX, this.ScaleY);

            return sprite;
        }

        public void SetScaleFromSize(int width, int height)
        {
            this.ScaleX = (float)width / 2048;
            this.ScaleY = (float)height / 2048;
        }


        public bool IsPointInside(int x, int y)
        {
            return false;
        }

        public void SetSize(int positionX, int positionY, int width, int height)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.SetScaleFromSize(width, height);
        }

        public IRenderable GetActor()
        {
            return this.Actor;
        }
    }
}
