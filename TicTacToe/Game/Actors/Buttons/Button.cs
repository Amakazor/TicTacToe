using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal abstract class Button : Actor, IClickable
    {
        public Button(Position position, Gamestate gamestate) : base(position, gamestate) { }
        public abstract void OnClick(MouseButtonEventArgs args);
    }
}