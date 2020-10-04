namespace TicTacToe.Game.Data
{
    public struct Statistic
    {
        int Player;
        int Total;
        int Won;
        int Lost;
        int Draws;

        public Statistic(int player, int total, int won, int lost, int draws)
        {
            Player = player;
            Total = total;
            Won = won;
            Lost = lost;
            Draws = draws;
        }
    }
}