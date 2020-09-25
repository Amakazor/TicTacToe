using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using TicTacToe.Game;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            engine.Loop();
        }
    }
}
