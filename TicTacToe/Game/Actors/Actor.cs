using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    abstract class Actor : IRenderable
    {
        public  Gamestate Gamestate { get; protected set; }
        public  Position Position { get; protected set; }

        public Actor(Position position, Gamestate gamestate)
        {
            Position = position;
            Gamestate = gamestate;
        }
        public Position CalculatePosition(Position basePosition)
        {
            const double baseWidth = 1000.0D;
            const double baseHeight = 1000.0D;
            ScreenSize screenSize = Gamestate.ScreenSize;

            return new Position
            {
                X = (int)(screenSize.MarginLeft + (uint)Math.Floor(screenSize.Width / baseWidth * basePosition.X)),
                Y = (int)(screenSize.MarginTop + (uint)Math.Floor(screenSize.Height / baseHeight * basePosition.Y)),
                Width = (int)Math.Floor(screenSize.Width / baseWidth * basePosition.Width),
                Height = (int)Math.Floor(screenSize.Height / baseHeight * basePosition.Height),
            };
        }
        public abstract List<IRenderObject> GetRenderObjects();
    }
}
