namespace TicTacToe.Game.Data
{
    public struct Statistic
    {
        public Statistic(int total, int won, int lost, int draws)
        {
            Total = total;
            Won = won;
            Lost = lost;
            Draws = draws;
        }

        public int Total;
        public int Won;
        public int Lost;
        public int Draws;
    }
}