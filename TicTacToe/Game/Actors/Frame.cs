using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    class Frame : Actor
    {
        public Texture Texture { get; }

        public Frame(Texture texture, Position position, Gamestate gamestate) : base(position, gamestate)
        {
            Texture = texture;
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> { new RenderFrame(CalculateScreenSpacePosition(Position), this, Texture) };
        }
    }
}
