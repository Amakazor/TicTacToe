using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TicTacToe.Game.Actors;

namespace TicTacToe.Game.Data
{
    class PlayersManager
    {
        public Dictionary<int, Player> Players { get; private set; }

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
            //Players = new Dictionary<int, Player>();

            XDocument playersConfig = XDocument.Load("assets/data/Players.xml");

            Players = (from player in playersConfig.Descendants("player")
                       select new {
                           Id = int.Parse(player.Element("id").Value),
                           Name = player.Element("name").Value,
                           Texture = TextureAtlas.TexturesDictionary[TextureType.Symbol][player.Element("symbol").Value],
                           Color = new Color(
                               byte.Parse(player.Element("color").Element("r").Value), 
                               byte.Parse(player.Element("color").Element("r").Value), 
                               byte.Parse(player.Element("color").Element("r").Value)
                            )
                       }).ToDictionary( o => o.Id, o => new Player(o.Name, o.Texture, o.Color, Gamestate));
        }

        private void SavePlayers()
        {
            StringBuilder sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb.Append("<players>" + '\n');
            foreach (KeyValuePair<int, Player> idPlayerPair in Players)
            {
                sb.Append(idPlayerPair.Value.SerializeToXML(idPlayerPair.Key));
            }
            sb.Append("</players>");

            File.WriteAllText("assets/data/Players.xml", sb.ToString());
        }

        public int AddAndSavePlayer(Player playerToAdd)
        {
            int newId = Players.Keys.Max() + 1;
            Players.Add(newId, playerToAdd);
            SavePlayers();
            return newId;
        }
    }
}
