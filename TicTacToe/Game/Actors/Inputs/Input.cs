using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Inputs
{
    internal abstract class Input : Actor, IClickable
    {
        public int Id { get; private set; }

        public Input(Position position, Gamestate gamestate, int id) : base(position, gamestate)
        {
            Id = id;
        }

        public abstract bool OnClick(MouseButtonEventArgs args);
    }
}