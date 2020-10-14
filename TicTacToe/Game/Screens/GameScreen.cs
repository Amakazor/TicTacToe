using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    internal class GameScreen : Screen, IScreen
    {
        public Board Board { get; private set; }

        public GameScreen(Gamestate gamestate, PlayersManager playersManager) : base(gamestate, playersManager, ScreenType.Game)
        {
            MessageBus.Instance.Register(MessageType.FieldChanged, OnFieldChange);
            Gamestate.boardstate = Boardstate.NotResolved;

            Board = new Board(gamestate.BoardSize, new Position(25, 25, 950, 950), gamestate, PlayersManager);
        }

        private void OnFieldChange(object sender, EventArgs eventArgs)
        {
            Boardstate boardState = Board.CheckBoard();

            if (boardState == Boardstate.NotResolved)
            {
                PlayersManager.ChangePlayer();
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
            Board.RecalculateComponentsPositions();
            return Board.GetRenderObjects();
        }

        public override void Dispose()
        {
            MessageBus.Instance.Unregister(MessageType.FieldChanged, OnFieldChange);
        }
    }
}