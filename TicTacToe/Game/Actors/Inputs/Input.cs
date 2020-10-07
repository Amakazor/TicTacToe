using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Inputs
{
    abstract class Input : Actor, IClickable
    {
        public int Id { get; private set; }

        public Input(Position position, Gamestate gamestate, int id) : base(position, gamestate) {
            Id = id;
        }

        public abstract void OnClick(MouseButtonEventArgs args);
    }
}
