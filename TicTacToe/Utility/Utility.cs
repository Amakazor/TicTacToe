namespace TicTacToe.Utility
{
    static class Utility
    {
        public static bool IntBetweenInclusive(int input, int min, int max)
        {
            return (min <= input) && (input <= max);
        }

        public static int Square(int x)
        {
            return x * x;
        }
    }
}