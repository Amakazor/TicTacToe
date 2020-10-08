using SFML.Graphics;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    internal struct SymbolData
    {
        public Texture texture;
        public Color color;
    }

    internal class Symbol : Actor
    {
        public Texture Texture { get; }
        public Color Color { get; set; }

        private RenderSymbol SymbolSymbol;

        public Symbol(SymbolData symbolData, Gamestate gamestate) : this(symbolData.texture, symbolData.color, gamestate) { }

        public Symbol(SymbolData symbolData, Position position, Gamestate gamestate) : this(symbolData.texture, symbolData.color, position, gamestate) { }

        public Symbol(Texture texture, Color color, Gamestate gamestate) : this(texture, color, new Position(0, 0, 0, 0), gamestate) { }

        public Symbol(Texture texture, Color color, Position position, Gamestate gamestate) : base(position, gamestate)
        {
            Texture = texture;
            Color = color;

            SymbolSymbol = new RenderSymbol(new Position(), this, Texture, Color);
            RecalculateComponentsPositions();
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> { SymbolSymbol };
        }

        public override void RecalculateComponentsPositions()
        {
            SymbolSymbol.SetPosition(CalculateScreenSpacePosition(Position));
        }
    }
}