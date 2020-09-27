using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    class PlayersScreen : Screen
    {
        public List<Actor> Actors { get; set; }
        public PlayersScreen(Gamestate gamestate) : base(gamestate, ScreenType.Players)
        {
            Actors = new List<Actor>();
            switch (Gamestate.PlayersInGame.Count)
            {
                case 0:
                    Actors.Add(new ScreenChangeButton(new Position(25, 50, 450, 100), new Position(50, 30, 0, 30), Gamestate, "Select player", ScreenType.PlayerSelectionScreen));
                    Actors.Add(new ScreenChangeButton(new Position(525, 50, 450, 100), new Position(50, 30, 0, 30), Gamestate, "Create player", ScreenType.PlayerSelectionScreen));
                    break;
                case 1:
                    Actors.Add(new ActionButton(new Position(25, 50, 450, 100), new Position(50, 30, 0, 30), Gamestate, Gamestate.GetPlayerByPlayersInGameIndex(0).Nickname, (_) => { Gamestate.RemovePlayerFromGame(0); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    Actors.Add(new ScreenChangeButton(new Position(525, 50, 450, 100), new Position(50, 30, 0, 30), Gamestate, "Select second player", ScreenType.PlayerSelectionScreen));
                    break;
                case 2:
                    Actors.Add(new ActionButton(new Position(25, 50, 450, 100), new Position(50, 30, 0, 30), Gamestate, Gamestate.GetPlayerByPlayersInGameIndex(0).Nickname, (_) => { Gamestate.RemovePlayerFromGame(0); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    Actors.Add(new ActionButton(new Position(525, 50, 450, 100), new Position(50, 30, 0, 30), Gamestate, Gamestate.GetPlayerByPlayersInGameIndex(1).Nickname, (_) => { Gamestate.RemovePlayerFromGame(1); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    break;
            }
        }

        public override void Dispose()
        {}

        public override List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();

            foreach (Actor actor in Actors)
            {
                renderObjects.AddRange(actor.GetRenderObjects());
            }

            return renderObjects;
        }
    }
}
