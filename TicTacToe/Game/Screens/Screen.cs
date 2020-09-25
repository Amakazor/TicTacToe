using TicTacToe.Game.Data;

namespace TicTacToe.Game.Screens
{
    class Screen
    {
        public Gamestate Gamestate { get; protected set; }
        public EScreens EScreen { get; protected set; }

        public Screen(Gamestate gamestate, EScreens eScreen)
        {
            Gamestate = gamestate;
            EScreen = eScreen;
        }
    }
}
