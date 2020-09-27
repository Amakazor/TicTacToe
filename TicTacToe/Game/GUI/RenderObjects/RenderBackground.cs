using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using TicTacToe.Game.Actors;

namespace TicTacToe.Game.GUI.RenderObjects
{
    class RenderBackground : IRenderObject
    {
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Texture Texture { get; set; }

        public RenderBackground(int positionX, int positionY, int width, int height, Texture texture)
        {
            PositionX = positionX;
            PositionY = positionY;
            Width = width;
            Height = height;
            Texture = texture;
        }


        public IRenderable GetActor()
        {
            return null;
        }

        public Transformable GetShape()
        {
            Texture.Repeated = true;

            Sprite sprite = new Sprite(Texture);
            sprite.TextureRect = new IntRect(0, 0, Width, Height);

            return sprite;
        }

        public bool IsPointInside(int x, int y)
        {
            return false;
        }

        public void SetSize(int positionX, int positionY, int width, int height)
        {
            PositionX = positionX;
            PositionY = positionY;
            Width = width;
            Height = height;
        }
    }
}
