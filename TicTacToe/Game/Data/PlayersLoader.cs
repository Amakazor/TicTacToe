using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;
using TicTacToe.Game.Actors;

namespace TicTacToe.Game.Data
{
    static class PlayersLoader
    {
        public static Dictionary<int, Player> LoadPlayers(Textures textureAtlas)
        {
            Dictionary<int, Player> Players = new Dictionary<int, Player>();

            XmlDocument playersConfig = new XmlDocument();

            playersConfig.Load("assets/data/Players.xml");

            if (playersConfig.DocumentElement.ChildNodes.Count == 0)
            {
                //TODO: add exception;
                throw new Exception("");
            }
            else
            {
                foreach (XmlNode playerData in playersConfig.DocumentElement.ChildNodes)
                {
                    if (!Players.ContainsKey(int.Parse(playerData.Attributes.GetNamedItem("id").Value)))
                    {
                        Color color = new Color(byte.Parse(playerData.Attributes.GetNamedItem("r").Value),
                            byte.Parse(playerData.Attributes.GetNamedItem("g").Value),
                            byte.Parse(playerData.Attributes.GetNamedItem("b").Value));

                        Texture texture = textureAtlas.TexturesDictionary[playerData.Attributes.GetNamedItem("symbol").Value];

                        SymbolData symbolData = new SymbolData();
                        symbolData.color = color;
                        symbolData.texture = texture;

                        Player player = new Player(playerData.Attributes.GetNamedItem("name").Value, symbolData);

                        Players.Add(int.Parse(playerData.Attributes.GetNamedItem("id").Value), player);
                    }
                    else
                    {
                        //TODO: add exception;
                        throw new Exception("");
                    }
                }
            }

            return Players;
        }
    }
}
