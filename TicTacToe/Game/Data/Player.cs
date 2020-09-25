using SFML.Graphics;
using System;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.Data
{
    class Player
    {
        public string Nickname { get; private set; }
        public SymbolData SymbolData;

        public Player(string nickname, Texture texture, Color color)
        {
            Nickname = nickname;
            SymbolData = new SymbolData
            {
                texture = texture,
                color = color
            };
        }

        public Player(string nickname, SymbolData symbolData)
        {
            Nickname = nickname;
            SymbolData = symbolData;
        }

        public Symbol GetSymbol()
        {
            return new Symbol(SymbolData);
        }

        public Symbol GetSymbol(Position position)
        {
            return new Symbol(SymbolData, position);
        }
    }
}
