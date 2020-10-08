using SFML.Graphics;
using SFML.System;
using System;

namespace TicTacToe.Game.GUI.Shapes
{
    internal class RoundedRectangleShape : Shape
    {
        private Vector2f mySize;
        private float myRadius;
        private uint myPointCount;

        public RoundedRectangleShape(Vector2f size, float radius) : this(size, radius, 10) { }

        public RoundedRectangleShape(Vector2f size, float radius, uint pointCount)
        {
            Size = size;
            Radius = radius;
            PointCount = pointCount;
        }

        public Vector2f Size
        {
            get { return mySize; }
            set { mySize = value; Update(); }
        }

        public float Radius
        {
            get { return myRadius; }
            set { myRadius = value; Update(); }
        }

        public uint PointCount
        {
            get { return myPointCount; }
            set { myPointCount = value * 4; Update(); }
        }

        public override Vector2f GetPoint(uint index)
        {
            float angle = (float)(index * 2 * Math.PI / myPointCount - Math.PI / 2);
            float x = (float)Math.Cos(angle) * myRadius + myRadius;
            float y = (float)Math.Sin(angle) * myRadius + myRadius;

            if (x > myRadius) x += mySize.X - 2 * myRadius;
            if (y > myRadius) y += mySize.Y - 2 * myRadius;

            return (new Vector2f(x, y));
        }

        public override uint GetPointCount()
        {
            return myPointCount;
        }
    }
}