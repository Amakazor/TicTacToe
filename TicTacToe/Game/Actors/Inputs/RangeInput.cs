using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Inputs
{
    internal class RangeInput : Input, IHoldable
    {
        public int Value;
        public int MinValue;
        public int MaxValue;
        public Color Color;

        private RenderRectangle CollisionBox;
        private RenderRectangle Bar;
        private RenderCircle Circle;
        private AlignedRenderText Text;

        private int LeftOffset;

        private Action<RangeInput, int> Action;

        public RangeInput(Position position, Gamestate gamestate, int startValue, int minValue, int maxValue, Color color, Action<RangeInput, int> action, int id) : base(position, gamestate, id)
        {
            Value = startValue;
            MinValue = minValue;
            MaxValue = maxValue;
            Color = color;
            Action = action;

            LeftOffset = CalculateLeftOffset();

            CollisionBox = new RenderRectangle(CalculateScreenSpacePosition(new Position(Position.X, Position.Y, Position.Width, Position.Height)), this, new Color(0, 0, 0, 0));
            Bar = new RenderRectangle(CalculateScreenSpacePosition(new Position(Position.X, Position.Y + Position.Height / 3, Position.Width, Position.Height / 3)), this, Color);
            Circle = new RenderCircle(CalculateScreenSpacePosition(new Position(Position.X + LeftOffset, Position.Y, 0, Position.Height / 2)), this, Color.White, Color, Position.Height / 16);
            Text = new AlignedRenderText(CalculateScreenSpacePosition(new Position(Position.X + LeftOffset, Position.Y, Position.Height, Position.Height)), new Vector2f(0, 0), CalculateScreenSpaceHeight(Position.Height / 2), TextPosition.Middle, TextPosition.Middle, this, Color.Black, Value.ToString());
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            RecalculateComponentsPositions();
            return new List<IRenderObject> { CollisionBox, Bar, Circle, Text };
        }

        public override void OnClick(MouseButtonEventArgs args)
        {
            MessageBus.Instance.PostEvent(MessageType.LoseFocus, this, args);

            Position screenSpacePosition = CalculateScreenSpacePosition(Position);
            Value = (int)(((double)(args.X - screenSpacePosition.X)) / (double)screenSpacePosition.Width * ((double)(MaxValue - MinValue))) + MinValue;

            Text.Text = Value.ToString();

            RecalculateComponentsPositions();

            Action.Invoke(this, Value);

            Console.WriteLine(args.X);
        }

        private int CalculateLeftOffset()
        {
            return (int)((double)(Value - MinValue) / (double)(MaxValue - MinValue) * (double)Position.Width) - Position.Height / 2;
        }

        private void RecalculateComponentsPositions()
        {
            LeftOffset = CalculateLeftOffset();

            CollisionBox.SetPosition(CalculateScreenSpacePosition(new Position(Position.X, Position.Y, Position.Width, Position.Height)));
            Bar.SetPosition(CalculateScreenSpacePosition(new Position(Position.X, Position.Y + Position.Height / 3, Position.Width, Position.Height / 3)));
            Circle.SetPosition(CalculateScreenSpacePosition(new Position(Position.X + LeftOffset, Position.Y, 0, Position.Height / 2)));
            Text.SetPosition(CalculateScreenSpacePosition(new Position(Position.X + LeftOffset, Position.Y, Position.Height, Position.Height)));
        }
    }
}