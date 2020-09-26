using SFML.Graphics;
using System;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.Data
{
    class Player
    {
        public string Nickname { get; private set; }
        public Gamestate Gamestate { get; private set; }
        public SymbolData SymbolData;

        public Player(string nickname, Texture texture, Color color, Gamestate gamestate)
        {
            Nickname = nickname;
            Gamestate = gamestate;
            SymbolData = new SymbolData
            {
                texture = texture,
                color = color
            };
        }

        public Player(string nickname, SymbolData symbolData, Gamestate gamestate)
        {
            Nickname = nickname;
            SymbolData = symbolData;
            Gamestate = gamestate;
        }

        public Symbol GetSymbol()
        {
            return new Symbol(SymbolData, Gamestate);
        }

        public Symbol GetSymbol(Position position)
        {
            return new Symbol(SymbolData, position, Gamestate);
        }
    }
}
