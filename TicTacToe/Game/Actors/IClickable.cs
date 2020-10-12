using SFML.Window;

namespace TicTacToe.Game.Actors
{
    internal interface IClickable
    {
        public bool OnClick(MouseButtonEventArgs args);
    }
}