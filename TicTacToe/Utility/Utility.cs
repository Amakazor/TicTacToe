namespace TicTacToe.Utility
{
    internal static class Utility
    {
        public static bool IntBetweenInclusive(int input, int min, int max)
        {
            return (min <= input) && (input <= max);
        }
    }
}