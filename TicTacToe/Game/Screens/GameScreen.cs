using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.GUI.RenderObjects;
using System;
using TicTacToe.Utility;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;

namespace TicTacToe.Game.Screens
{
    class GameScreen : Screen, IScreen
    {
        public Board Board { get; private set; }

        public GameScreen(Gamestate gamestate) : base(gamestate, ScreenType.Game)
        {
            MessageBus.Instance.Register(MessageType.FieldChanged, OnFieldChange);
            Gamestate.boardstate = Boardstate.NotResolved;

            Board = new Board(gamestate.BoardSize, new Position(0, 0, 1000, 1000), gamestate);
        }

        private void OnFieldChange(object sender, EventArgs eventArgs)
        {
            Boardstate boardState = Board.CheckBoard();

            if (boardState == Boardstate.NotResolved)
            {
                Gamestate.ChangePlayer();
                MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());

            }
            else
            {
                Gamestate.boardstate = boardState;
                MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.Results });
            }
        }

        public override List<IRenderObject> GetRenderData()
        {
            return Board.GetRenderObjects();
        }

        public override void Dispose()
        {
            MessageBus.Instance.Unregister(MessageType.FieldChanged, OnFieldChange);
        }
    }
}
