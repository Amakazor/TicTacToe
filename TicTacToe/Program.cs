using System;
using System.Diagnostics;
using TicTacToe.Game;
using TicTacToe.Utility.Exceptions;

namespace TicTacToe
{
    internal class Program
    {
        private static void Main(string[] args)
        {
			try
			{
				Engine engine = new Engine();
				engine.Loop();
			}
            catch (Exception e)
            {
                #if DEBUG
                    throw e;
                #else
                    throw new FileMissingOrCorruptedException("One or more of the game files");
                #endif
            }
        }
    }
}