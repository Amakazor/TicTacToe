using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Screens;
using TicTacToe.Utility;
using static TicTacToe.Utility.Utility;

namespace TicTacToe.Game.Data
{
    class Gamestate
    {
        public Textures TextureAtlas { get; private set; }
        public Dictionary<int, Player> Players { get; private set; }

        public int BoardSize { get; set; }
        public List<int> PlayersInGame { get; private set; }
        public Boardstate boardstate { get; set; }

        public int CurrentPlayer { get; private set; }
        public IScreen CurrentScreen { get; set; }
        public EScreens PreviousScreen { get; set; }

        public Gamestate()
        {
            TextureAtlas = new Textures();
            Players = PlayersLoader.LoadPlayers(this.TextureAtlas);

            BoardSize = 0;
            PlayersInGame = new List<int>();

            CurrentPlayer = 0;
            CurrentScreen = null;
            PreviousScreen = EScreens.Pregame;

        }

        public void SetCurrentPlayer(int newPlayer)
        {
            if (PlayersInGame.Contains(newPlayer) && CurrentPlayer != newPlayer)
            {
                this.CurrentPlayer = newPlayer;
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

        public Symbol GetPlayersSymbolByPlayerID(int PlayerID)
        {
            if (Players.ContainsKey(PlayerID))
            {
                return Players[PlayerID].GetSymbol();
            }
            else throw new ArgumentException("Player with id " + PlayerID + " doesn't exist", "id");
        }
        
        public Symbol GetPlayersSymbolByPlayerID(int PlayerID, Position position)
        {
            if (Players.ContainsKey(PlayerID))
            {
                return Players[PlayerID].GetSymbol(position);
            }
            else throw new ArgumentException("Player with id " + PlayerID + " doesn't exist", "id");
        }

        public Symbol GetCurrentPlayersSymbol()
        {
            return GetPlayersSymbolByPlayerID(CurrentPlayer);
        }
        
        public Symbol GetCurrentPlayersSymbol(Position position)
        {
            return GetPlayersSymbolByPlayerID(CurrentPlayer, position);
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

        public bool CanStartGame()
        {
            return PlayersInGame.Count == 2 && IntBetweenInclusive(BoardSize, 2, int.MaxValue);
        }
    }
}
