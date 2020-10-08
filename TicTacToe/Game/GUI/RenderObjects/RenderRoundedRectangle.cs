using SFML.Graphics;
using SFML.System;
using TicTacToe.Game.Actors;
using TicTacToe.Game.GUI.Shapes;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    internal class RenderRoundedRectangle : IRenderObject
    {
        public Position Position;
        public float Radius { get; set; }

        protected IRenderable Actor { get; }
        private Color FillColor { get; }
        private Color OutlineColor { get; }
        public float OutlineThickness { get; set; }

        public RenderRoundedRectangle(IRenderable actor, float radius) : this(new Position(), actor, radius) { }
        public RenderRoundedRectangle(Position position, IRenderable actor, float radius) : this(position, actor, new Color(), radius) { }
        public RenderRoundedRectangle(Position position, IRenderable actor, Color fillColor, float radius) : this(position, actor, fillColor, new Color(), 0, radius) { }
        public RenderRoundedRectangle(Position position, IRenderable actor, Color fillColor, Color outlineColor, float outlineThickness, float radius)
        {
            Position = position;
            Actor = actor;
            FillColor = fillColor;
            OutlineColor = outlineColor;
            OutlineThickness = outlineThickness;
            Radius = radius;
        }

        public void SetSize(int width, int height)
        {
            Position.Width = width;
            Position.Height = height;
        }

        public Transformable GetShape()
        {
            RoundedRectangleShape rectangle = new RoundedRectangleShape(new Vector2f(Position.Width, Position.Height), Radius);
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