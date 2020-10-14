using SFML.Graphics;
using System.Text;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.Data
{
    internal class Player
    {
        public string Nickname { get; set; }
        public Gamestate Gamestate { get; set; }
        public TextureManager TextureManager { get; }

        public SymbolData SymbolData;

        public Player(string nickname, Texture texture, Color color, Gamestate gamestate, TextureManager textureManager)
        {
            Nickname = nickname;
            Gamestate = gamestate;
            TextureManager = textureManager;
            SymbolData = new SymbolData
            {
                texture = texture,
                color = color
            };
        }

        public Player(string nickname, SymbolData symbolData, Gamestate gamestate, TextureManager textureManager)
        {
            Nickname = nickname;
            SymbolData = symbolData;
            Gamestate = gamestate;
            TextureManager = textureManager;
        }

        public Player()
        {
        }

        public Symbol GetSymbol()
        {
            return new Symbol(SymbolData, Gamestate);
        }

        public Symbol GetSymbol(Position position)
        {
            return new Symbol(SymbolData, position, Gamestate);
        }

        public string SerializeToXML(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\t" + " <player>" + '\n');
            sb.Append("\t\t" + "<id>" + id + "</id>" + '\n');
            sb.Append("\t\t" + "<name>" + Nickname + "</name>" + '\n');
            sb.Append("\t\t" + "<symbol>" + TextureManager.GetNameFromTexture(TextureType.Symbol, SymbolData.texture) + "</symbol>" + '\n');
            sb.Append("\t\t" + "<color>" + '\n');
            sb.Append("\t\t\t" + "<r>" + SymbolData.color.R + "</r>" + '\n');
            sb.Append("\t\t\t" + "<g>" + SymbolData.color.G + "</g>" + '\n');
            sb.Append("\t\t\t" + "<b>" + SymbolData.color.B + "</b>" + '\n');
            sb.Append("\t\t" + "</color>" + '\n');
            sb.Append("\t" + "</player>" + '\n');

            return sb.ToString();
        }
    }
}