using SFML.Graphics;
using System.Collections.Generic;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    struct SymbolData
    {
        public Texture texture;
        public Color color;
    }

    class Symbol : IRenderable
    {
        RenderSymbol renderSymbol;

        public Symbol(SymbolData symbolData) : this(symbolData.texture, symbolData.color) { }
        public Symbol(SymbolData symbolData, Position position) : this(symbolData.texture, symbolData.color, position) { }
        public Symbol(Texture texture, Color color) : this(texture, color, new Position(0, 0, 0, 0)) { }
        public Symbol(Texture texture, Color color, Position position)
        {
            renderSymbol = new RenderSymbol(position, this, texture, color);
        }

        public List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> { renderSymbol };
        }
    }
}
