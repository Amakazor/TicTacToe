using SFML.Window;

namespace TicTacToe.Game.Actors
{
    internal interface IClickable
    {
        public void OnClick(MouseButtonEventArgs args);
    }
}