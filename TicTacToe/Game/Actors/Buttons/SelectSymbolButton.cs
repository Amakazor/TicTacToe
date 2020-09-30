using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    class SelectSymbolButton : Button
    {
        public Symbol Symbol { get; private set; }
        public SelectSymbolButton(Position position, Position relativeTextPosition, Gamestate gamestate, string text, Symbol symbol) : base(position, relativeTextPosition, gamestate, text)
        {
            Symbol = symbol;
        }

        public override void OnClick(MouseButtonEventArgs args)
        {
            Gamestate.NewPlayer.SymbolData.texture = Symbol.Texture;
        }
    }
}
