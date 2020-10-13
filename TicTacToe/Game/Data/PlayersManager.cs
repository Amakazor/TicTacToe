using SFML.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TicTacToe.Game.Data
{
    internal class PlayersManager
    {
        public Dictionary<int, Player> Players { get; private set; }
        public int HighestId { get; private set; }

        private TextureManager TextureAtlas { get; }
        private Gamestate Gamestate { get; }

        public PlayersManager(TextureManager textureAtlas, Gamestate gamestate)
        {
            TextureAtlas = textureAtlas;
            Gamestate = gamestate;

            LoadPlayers();
        }

        private void LoadPlayers()
        {
            XDocument playersConfig = XDocument.Load("assets/data/Players.xml");

            Players = (from player in playersConfig.Descendants("player")
                       select new
                       {
                           Id = int.Parse(player.Element("id").Value),
                           Name = player.Element("name").Value,
                           Texture = TextureAtlas.TexturesDictionary[TextureType.Symbol][player.Element("symbol").Value],
                           Color = new Color(
                               byte.Parse(player.Element("color").Element("r").Value),
                               byte.Parse(player.Element("color").Element("g").Value),
                               byte.Parse(player.Element("color").Element("b").Value)
                            )
                       }).ToDictionary(o => o.Id, o => new Player(o.Name, o.Texture, o.Color, Gamestate));

            HighestId = int.Parse(playersConfig.Descendants("highestid").First().Value);
        }

        private void SavePlayers()
        {
            StringBuilder sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + '\n');
            sb.Append("<players>" + '\n');
            sb.Append("<highestid>" + HighestId + "</highestid>" + '\n');
            foreach (KeyValuePair<int, Player> idPlayerPair in Players)
            {
                sb.Append(idPlayerPair.Value.SerializeToXML(idPlayerPair.Key));
            }
            sb.Append("</players>");

            File.WriteAllText("assets/data/Players.xml", sb.ToString());
        }

        public int AddAndSavePlayer(Player playerToAdd)
        {
            HighestId++;
            Players.Add(HighestId, playerToAdd);
            SavePlayers();
            return HighestId;
        }

        public void DeletePlayer(int playerId)
        {
            if (Players.ContainsKey(playerId))
            {
                Players.Remove(playerId);
            }
            SavePlayers();
        }
    }
}