using TicTacToe.Game.Screens;
using static TicTacToe.Utility.Utility;

namespace TicTacToe.Game.Data
{
    internal class Gamestate
    {
        public int BoardSize { get; set; }
        public Boardstate boardstate { get; set; }

        public Screen CurrentScreen { get; set; }
        public ScreenType PreviousScreen { get; set; }
        public ScreenType SecondPreviousScreen { get; set; }

        public PlayersManager PlayersManager { private get; set; }
        public ScreenSize ScreenSize { get; set; }

        public Gamestate()
        {
            BoardSize = 0;

            CurrentScreen = null;
            PreviousScreen = ScreenType.Pregame;
        }

        public bool CanStartGame()
        {
            return PlayersManager.PlayersInGame.Count == 2 && IntBetweenInclusive(BoardSize, 2, int.MaxValue);
        }

        public void Clear()
        {
            PlayersManager.Clear();

            boardstate = Boardstate.NotResolved;
            BoardSize = 0;
        }
    }
}