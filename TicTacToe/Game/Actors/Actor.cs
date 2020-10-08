using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    internal abstract class Actor : IRenderable
    {
        private const double BaseWidth = 1000.0D;
        private const double BaseHeight = 1000.0D;

        public Gamestate Gamestate { get; protected set; }
        public Position Position { get; protected set; }

        public Actor(Position position, Gamestate gamestate)
        {
            Position = position;
            Gamestate = gamestate;
        }

        public Position CalculateScreenSpacePosition(Position basePosition)
        {
            ScreenSize screenSize = Gamestate.ScreenSize;

            return new Position
            {
                X = (int)(screenSize.MarginLeft + (uint)Math.Floor(screenSize.Width / BaseWidth * basePosition.X)),
                Y = (int)(screenSize.MarginTop + (uint)Math.Floor(screenSize.Height / BaseHeight * basePosition.Y)),
                Width = (int)Math.Floor(screenSize.Width / BaseWidth * basePosition.Width),
                Height = (int)Math.Floor(screenSize.Height / BaseHeight * basePosition.Height),
            };
        }

        public abstract List<IRenderObject> GetRenderObjects();

        public int CalculateScreenSpaceHeight(int baseHeight)
        {
            ScreenSize screenSize = Gamestate.ScreenSize;

            return (int)Math.Floor(screenSize.Height / BaseHeight * baseHeight);
        }

        public double CalculateScreenSpaceHeight(double baseHeight)
        {
            ScreenSize screenSize = Gamestate.ScreenSize;

            return screenSize.Height / BaseHeight * baseHeight;
        }

        public abstract void RecalculateComponentsPositions();
    }
}