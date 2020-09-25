using SFML.Window;

namespace TicTacToe.Game.Actors
{
    interface IClickable
    {
        public void OnClick(MouseButtonEventArgs args);
    }
}
