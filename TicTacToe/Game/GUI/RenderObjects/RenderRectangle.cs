using SFML.Graphics;
using SFML.System;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    class RenderRectangle : IRenderObject
    {
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        protected IRenderable Actor { get; set; }
        private Color FillColor { get; set; }

        public RenderRectangle(IRenderable actor) : this(0, 0, 0, 0, actor) {}
        public RenderRectangle(Position position, IRenderable actor) : this(position, actor, Color.White) { }
        public RenderRectangle(Position position, IRenderable actor, Color fillColor) : this(position.X, position.Y, position.Width, position.Height, actor, fillColor) { }
        public RenderRectangle(int positionX, int positionY, int width, int height, IRenderable actor) : this(positionX, positionY, width, height, actor, Color.White) {}
        public RenderRectangle(int positionX, int positionY, int width, int height, IRenderable actor, Color fillColor)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
            this.Actor = actor;
            this.FillColor = fillColor;
        }

        public void SetSize(int positionX, int positionY, int width, int height)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
        }

        public Transformable GetShape()
        {
            RectangleShape rectangle = new RectangleShape(new Vector2f(this.Width, this.Height));
            rectangle.Position = new Vector2f(this.PositionX, this.PositionY);
            rectangle.FillColor = this.FillColor;
            rectangle.OutlineColor = Color.Cyan;
            rectangle.OutlineThickness = 2;

            return rectangle;
        }

        public bool IsPointInside(int x, int y)
        {
            return (this.PositionX <= x) && (this.PositionX + this.Width >= x) && (this.PositionY <= y) && (this.PositionY + this.Height >= y);
        }

        public IRenderable GetActor()
        {
            return this.Actor;
        }
    }
}
