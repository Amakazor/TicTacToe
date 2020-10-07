using SFML.Graphics;
using SFML.System;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    class RenderRectangle : IRenderObject
    {
        public Position Position;
        protected IRenderable Actor { get; }
        private Color FillColor { get; }

        public RenderRectangle(IRenderable actor) : this(new Position(), actor) {}
        public RenderRectangle(Position position, IRenderable actor) : this(position, actor, Color.White) { }
        public RenderRectangle(Position position, IRenderable actor, Color fillColor)
        {
            Position = position;
            Actor = actor;
            FillColor = fillColor;
        }


        public void SetSize(int width, int height)
        {
            Position.Width = width;
            Position.Height = height;
        }

        public Transformable GetShape()
        {
            RectangleShape rectangle = new RectangleShape(new Vector2f(Position.Width, Position.Height));
            rectangle.Position = new Vector2f(Position.X, Position.Y);
            rectangle.FillColor = this.FillColor;
            rectangle.OutlineColor = Color.Cyan;
            rectangle.OutlineThickness = 2;

            return rectangle;
        }

        public bool IsPointInside(int x, int y)
        {
            return (Position.X <= x) && (Position.X + Position.Width >= x) && (Position.Y <= y) && (Position.Y + Position.Height >= y);
        }

        public IRenderable GetActor()
        {
            return Actor;
        }

        public void SetPosition(Position position)
        {
            Position = position;
        }
    }
}
