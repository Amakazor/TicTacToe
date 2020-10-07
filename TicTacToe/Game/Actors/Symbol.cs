using SFML.Graphics;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    struct SymbolData
    {
        public Texture texture;
        public Color color;
    }

    class Symbol : Actor
    {
        public Texture Texture { get; }
        public Color Color { get; set; }

        public Symbol(SymbolData symbolData, Gamestate gamestate) : this(symbolData.texture, symbolData.color, gamestate) { }
        public Symbol(SymbolData symbolData, Position position, Gamestate gamestate) : this(symbolData.texture, symbolData.color, position, gamestate) { }
        public Symbol(Texture texture, Color color, Gamestate gamestate) : this(texture, color, new Position(0, 0, 0, 0), gamestate) { }
        public Symbol(Texture texture, Color color, Position position, Gamestate gamestate) : base(position, gamestate)
        {
            Texture = texture;
            Color = color;
        }

        

        public override List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> { new RenderSymbol(CalculateScreenSpacePosition(Position), this, Texture, Color) };
        }
    }
}
