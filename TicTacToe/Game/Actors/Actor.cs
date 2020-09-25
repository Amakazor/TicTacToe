using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    abstract class Actor
    {
        public  Gamestate Gamestate { get; protected set; }
        public  Position Position { get; protected set; }

        public Actor(Position position, Gamestate gamestate)
        {
            Position = position;
            Gamestate = gamestate;
        }
    }
}
