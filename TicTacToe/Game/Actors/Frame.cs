using SFML.Graphics;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    internal class Frame : Actor
    {
        private RenderFrame FrameFrame;

        public Frame(Texture texture, Position position, Gamestate gamestate) : base(position, gamestate)
        {
            FrameFrame = new RenderFrame(new Position(), this, texture);
            RecalculateComponentsPositions();
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> { FrameFrame };
        }

        public override void RecalculateComponentsPositions()
        {
            FrameFrame.SetPosition(CalculateScreenSpacePosition(Position));
        }
    }
}