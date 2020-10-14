using SFML.Graphics;
using SFML.System;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    internal class RenderRectangle : IRenderObject
    {
        public Position Position;
        protected IRenderable Actor { get; }
        private Color FillColor { get; }
        private Color OutlineColor { get; }
        private float OutlineThickness { get; }

        public RenderRectangle(Position position, IRenderable actor, Color fillColor) : this(position, actor, fillColor, new Color(), 0)
        {
        }

        public RenderRectangle(Position position, IRenderable actor, Color fillColor, Color outlineColor, float outlineThickness)
        {
            Position = position;
            Actor = actor;
            FillColor = fillColor;
            OutlineColor = outlineColor;
            OutlineThickness = outlineThickness;
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
            rectangle.FillColor = FillColor;
            rectangle.OutlineColor = OutlineColor;
            rectangle.OutlineThickness = OutlineThickness;

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