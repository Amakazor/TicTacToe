using TicTacToe.Game;

namespace TicTacToe
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Engine engine = new Engine();
            engine.Loop();
        }
    }
}