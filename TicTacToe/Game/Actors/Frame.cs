using SFML.Graphics;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    internal class Frame : Actor
    {
        private RenderRoundedRectangle FrameRectangle;

        public Frame(Gamestate gamestate) : base(new Position(0, 0, 1000, 1000), gamestate)
        {
            FrameRectangle = new RenderRoundedRectangle(new Position(), this, new Color(245, 245, 245), new Color(120, 160, 255), CalculateScreenSpaceHeight(10), CalculateScreenSpaceHeight(20));
            RecalculateComponentsPositions();
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> { FrameRectangle };
        }

        public override void RecalculateComponentsPositions()
        {
            FrameRectangle.SetPosition(CalculateScreenSpacePosition(Position));
            FrameRectangle.Radius = (CalculateScreenSpaceHeight(20));
            FrameRectangle.OutlineThickness = (CalculateScreenSpaceHeight(10));
        }
    }
}