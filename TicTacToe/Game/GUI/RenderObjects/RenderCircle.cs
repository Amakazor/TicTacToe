using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    class RenderCircle : IRenderObject
    {
        public Position Position;
        protected IRenderable Actor { get; set; }
        private Color FillColor { get; set; }
        private Color OutlineColor { get; set; }
        public int OutlineWidth { get; }

        public RenderCircle(Position position, IRenderable actor, Color fillColor, Color outlineColor, int outlineWidth)
        {
            Position = position;
            Actor = actor;
            FillColor = fillColor;
            OutlineColor = outlineColor;
            OutlineWidth = outlineWidth;
        }

        public IRenderable GetActor()
        {
            return Actor;
        }

        public Transformable GetShape()
        {
            CircleShape circleShape = new CircleShape(Position.Height, 100);
            circleShape.Position = new Vector2f(Position.X, Position.Y);
            circleShape.FillColor = FillColor;
            circleShape.OutlineColor = OutlineColor;
            circleShape.OutlineThickness = OutlineWidth;

            return circleShape;
        }

        public bool IsPointInside(int x, int y)
        {
            return false;
        }

        public void SetSize(int width, int height)
        {
            Position.Width = width;
            Position.Height = height;
        }

        public void SetPosition(Position position)
        {
            Position = position;
        }
    }
}
