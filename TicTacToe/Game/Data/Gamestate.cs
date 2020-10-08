﻿using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Events;
using TicTacToe.Game.Screens;
using TicTacToe.Utility;
using static TicTacToe.Utility.Utility;

namespace TicTacToe.Game.Data
{
    internal class Gamestate
    {
        public ScreenSize ScreenSize { get; private set; }

        public TextureManager TextureAtlas { get; private set; }
        public PlayersManager PlayersManager { get; private set; }
        public StatisticsManager StatisticsManager { get; private set; }

        public int BoardSize { get; set; }
        public List<int> PlayersInGame { get; private set; }
        public Boardstate boardstate { get; set; }

        public int CurrentPlayer { get; private set; }
        public Screen CurrentScreen { get; set; }
        public ScreenType PreviousScreen { get; set; }

        public Player NewPlayer { get; set; }

        public Gamestate()
        {
            TextureAtlas = new TextureManager();
            PlayersManager = new PlayersManager(TextureAtlas, this);
            StatisticsManager = new StatisticsManager();

            BoardSize = 0;
            PlayersInGame = new List<int>();

            CurrentPlayer = 0;
            CurrentScreen = null;
            PreviousScreen = ScreenType.Pregame;

            MessageBus.Instance.Register(MessageType.ScreenResized, OnResize);
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
            if (playerIDs.Count == 2 && PlayersManager.Players.ContainsKey(playerIDs[0]) && PlayersManager.Players.ContainsKey(playerIDs[1]))
            {
                PlayersInGame = playerIDs;
            }
        }

        public Symbol GetPlayersSymbolByPlayerID(int playerID)
        {
            return GetPlayerByID(playerID).GetSymbol();
        }

        public Symbol GetPlayersSymbolByPlayerID(int playerID, Position position)
        {
            return GetPlayerByID(playerID).GetSymbol(position);
        }

        public Symbol GetCurrentPlayersSymbol()
        {
            return GetPlayersSymbolByPlayerID(CurrentPlayer);
        }

        public Symbol GetCurrentPlayersSymbol(Position position)
        {
            return GetPlayersSymbolByPlayerID(CurrentPlayer, position);
        }

        public Player GetPlayerByID(int playerID)
        {
            if (PlayersManager.Players.ContainsKey(playerID))
            {
                return PlayersManager.Players[playerID];
            }
            else throw new ArgumentException("Player with id " + playerID + " doesn't exist", "id");
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
            if (PlayersManager.Players.ContainsKey(PlayerID))
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
            return PlayersManager.Players[PlayersInGame[index]];
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

        public void OnResize(object sender, EventArgs sizeEventArgs)
        {
            if (sizeEventArgs is SizeEventArgs)
            {
                RecalculateScreenSize(((SizeEventArgs)sizeEventArgs).Width, ((SizeEventArgs)sizeEventArgs).Height);
            }
            else throw new ArgumentException("Wrong EventArgs given", "sizeEventArgs");
        }

        public void RecalculateScreenSize(uint width, uint height)
        {
            const double marginPercentage = 0.1D;
            uint smallerSize = width > height ? height : width;

            uint newWidth = (uint)Math.Floor(smallerSize * (1.0D - 2.0D * marginPercentage));
            uint newHeight = newWidth;
            uint newMarginTop = (uint)Math.Floor((height - newHeight) / 2.0D);
            uint newMarginLeft = (uint)Math.Floor((width - newWidth) / 2.0D);
            uint newTotalHeight = newHeight + newMarginTop * 2;
            uint newTotalWidth = newWidth + newMarginLeft * 2;

            ScreenSize = new ScreenSize(newWidth, newHeight, newMarginTop, newMarginLeft, newTotalWidth, newTotalHeight);

            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        public void ChangePlayer()
        {
            SetCurrentPlayer(CurrentPlayer == PlayersInGame[0] ? PlayersInGame[1] : PlayersInGame[0]);
        }

        public void Clear()
        {
            CurrentPlayer = 0;
            ClearPlayersInGame();
            boardstate = Boardstate.NotResolved;
            BoardSize = 0;
        }

        public bool ValidateNewPlayer()
        {
            return (NewPlayer.SymbolData.texture != null && NewPlayer.Nickname.Length >= 4 && NewPlayer.Nickname.Length <= 30);
        }

        public int SaveNewPlayer()
        {
            return PlayersManager.AddAndSavePlayer(NewPlayer);
        }
    }
}