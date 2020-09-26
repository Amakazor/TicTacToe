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

        public GameScreen(Gamestate gamestate) : base(gamestate, ScreenType.Game)
        {
            MessageBus.Instance.Register(MessageType.FieldChanged, OnFieldChange);

            Board = new Board(gamestate.BoardSize, new Position(0, 0, 1000, 1000), gamestate);
        }

        private void OnFieldChange(object sender, EventArgs eventArgs)
        {
            Boardstate boardState = Board.CheckBoard();

            if (boardState == Boardstate.NotResolved)
            {
                ChangePlayer();
            }
            else
            {
                Gamestate.boardstate = boardState;
            }

            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        private void ChangePlayer()
        {
            Gamestate.SetCurrentPlayer(Gamestate.CurrentPlayer == Gamestate.PlayersInGame[0] ? Gamestate.PlayersInGame[1] : Gamestate.PlayersInGame[0]);
        }

        public override List<IRenderObject> GetRenderData()
        {
            return Board.GetRenderObjects();
        }
    }
}
