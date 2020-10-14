using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.Data
{
    internal class PlayersManager
    {
        public Dictionary<int, Player> Players { get; private set; }
        public int HighestId { get; private set; }

        private TextureManager TextureManager { get; }
        private Gamestate Gamestate { get; }

        public List<int> PlayersInGame { get; private set; }
        public int CurrentPlayer { get; private set; }
        public Player NewPlayer { get; set; }

        public PlayersManager(TextureManager textureManager, Gamestate gamestate)
        {
            TextureManager = textureManager;
            Gamestate = gamestate;

            LoadPlayers();

            PlayersInGame = new List<int>();
            CurrentPlayer = 0;
        }

        private void LoadPlayers()
        {
            XDocument playersConfig = XDocument.Load("assets/data/Players.xml");

            Players = (from player in playersConfig.Descendants("player")
                       select new
                       {
                           Id = int.Parse(player.Element("id").Value),
                           Name = player.Element("name").Value,
                           Texture = TextureManager.TexturesDictionary[TextureType.Symbol][player.Element("symbol").Value],
                           Color = new Color(
                               byte.Parse(player.Element("color").Element("r").Value),
                               byte.Parse(player.Element("color").Element("g").Value),
                               byte.Parse(player.Element("color").Element("b").Value)
                            )
                       }).ToDictionary(o => o.Id, o => new Player(o.Name, o.Texture, o.Color, Gamestate, TextureManager));

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

        public void SetCurrentPlayer(int newPlayer)
        {
            if (PlayersInGame.Contains(newPlayer) && CurrentPlayer != newPlayer)
            {
                CurrentPlayer = newPlayer;
            }
            else throw new ArgumentException("Wrong player id given", "newPlayer");
        }

        public void SetPlayersInGame(List<int> playerIDs)
        {
            if (playerIDs.Count == 2 && Players.ContainsKey(playerIDs[0]) && Players.ContainsKey(playerIDs[1]))
            {
                PlayersInGame = playerIDs;
            }
        }

        public Symbol GetCurrentPlayersSymbol()
        {
            return GetPlayersSymbolByPlayerID(CurrentPlayer);
        }

        public Symbol GetCurrentPlayersSymbol(Position position)
        {
            return GetPlayersSymbolByPlayerID(CurrentPlayer, position);
        }

        public Player GetCurrentPlayer()
        {
            return GetPlayerByID(CurrentPlayer);
        }

        public void ClearPlayersInGame()
        {
            PlayersInGame.Clear();
        }

        public void AddPlayerToGame(int PlayerID)
        {
            if (Players.ContainsKey(PlayerID))
            {
                PlayersInGame.Add(PlayerID);
            }
        }

        public void RemovePlayerFromGame(int Index)
        {
            if (PlayersInGame.Count >= Index + 1)
            {
                PlayersInGame.RemoveAt(Index);
            }
        }

        public Player GetPlayerByPlayersInGameIndex(int index)
        {
            return Players[PlayersInGame[index]];
        }

        public void SetCurrentPlayerToFirstEntry()
        {
            if (PlayersInGame.Count > 0)
            {
                CurrentPlayer = PlayersInGame[0];
            }
            else throw new InvalidOperationException("Current player couldn't be set, because there are no players in game");
        }

        public void ChangePlayer()
        {
            SetCurrentPlayer(CurrentPlayer == PlayersInGame[0] ? PlayersInGame[1] : PlayersInGame[0]);
        }

        public bool ValidateNewPlayer()
        {
            return (NewPlayer.SymbolData.texture != null && NewPlayer.Nickname.Length >= 4 && NewPlayer.Nickname.Length <= 10);
        }

        public int SaveNewPlayer()
        {
            return AddAndSavePlayer(NewPlayer);
        }

        public Symbol GetPlayersSymbolByPlayerID(int playerID)
        {
            return GetPlayerByID(playerID).GetSymbol();
        }

        public Symbol GetPlayersSymbolByPlayerID(int playerID, Position position)
        {
            return GetPlayerByID(playerID).GetSymbol(position);
        }

        public Player GetPlayerByID(int playerID)
        {
            if (Players.ContainsKey(playerID))
            {
                return Players[playerID];
            }
            else throw new ArgumentException("Player with id " + playerID + " doesn't exist", "id");
        }

        public void Clear()
        {
            CurrentPlayer = 0;
            ClearPlayersInGame();
        }
    }
}