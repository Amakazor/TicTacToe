using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.GUI.RenderObjects;
using System;
using TicTacToe.Utility;
using TicTacToe.Game.Data;

namespace TicTacToe.Game.Screens
{
    class GameScreen : Screen, IScreen
    {
        public Board Board { get; private set; }

        public GameScreen(Gamestate gamestate) : base(gamestate, EScreens.Game)
        {
            MessageBus.Instance.Register(MessageType.FieldChanged, OnFieldChange);

            Board = new Board(gamestate.BoardSize, new Position(0, 0, 600, 600), gamestate);
        }

        private void OnFieldChange(object sender, EventArgs eventArgs)
        {
            int boardState = Board.CheckBoard();

            switch (boardState)
            {
                case -1:
                    break;
                case 0:
                    ChangePlayer();
                    break;
                case 1:
                    break;
            }

            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        private void ChangePlayer()
        {
            Gamestate.SetCurrentPlayer(Gamestate.CurrentPlayer == Gamestate.PlayersInGame[0] ? Gamestate.PlayersInGame[1] : Gamestate.PlayersInGame[0]);
        }

        public List<IRenderObject> GetRenderData()
        {
            return Board.GetRenderObjects();
        }

        public EScreens GetEScreen()
        {
            return EScreen;
        }
    }
}
