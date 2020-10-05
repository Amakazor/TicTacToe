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
                    Actors.Add(new ScreenChangeButton(new Position(25, 25, 450, 100), new Position(50, 30, 0, 30), Gamestate, "Select player", ScreenType.PlayerSelectionScreen));
                    Actors.Add(new ScreenChangeButton(new Position(525, 25, 450, 100), new Position(50, 30, 0, 30), Gamestate, "Create player", ScreenType.NewPlayer));
                    break;
                case 1:
                    Actors.Add(new ActionButton(new Position(25, 25, 450, 100), new Position(50, 30, 0, 30), Gamestate, Gamestate.GetPlayerByPlayersInGameIndex(0).Nickname, (_) => { Gamestate.RemovePlayerFromGame(0); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    Actors.Add(new ScreenChangeButton(new Position(525, 25, 450, 100), new Position(50, 30, 0, 30), Gamestate, "Select second player", ScreenType.PlayerSelectionScreen));
                    break;
                case 2:
                    Actors.Add(new ActionButton(new Position(25, 25, 450, 100), new Position(50, 30, 0, 30), Gamestate, Gamestate.GetPlayerByPlayersInGameIndex(0).Nickname, (_) => { Gamestate.RemovePlayerFromGame(0); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    Actors.Add(new ActionButton(new Position(525, 25, 450, 100), new Position(50, 30, 0, 30), Gamestate, Gamestate.GetPlayerByPlayersInGameIndex(1).Nickname, (_) => { Gamestate.RemovePlayerFromGame(1); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    break;
            }
            GenerateStatisticsDisplay();
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

        private void GenerateStatisticsDisplay()
        {
            Statistic statistic;

            switch (Gamestate.PlayersInGame.Count)
            {
                case 1:
                    statistic = Gamestate.StatisticsManager.LoadPlayersStatistics(Gamestate.PlayersInGame[0]);
                    GenerateStatisticsDisplayForOnePlayer(statistic);
                    break;
                case 2:
                    statistic = Gamestate.StatisticsManager.LoadPlayersStatistics(Gamestate.PlayersInGame[0], Gamestate.PlayersInGame[1]);
                    GenerateStatisticsDisplayForTwoPlayers(statistic);
                    break;
                default:
                    return;
            }
        }

        private void GenerateStatisticsDisplayForOnePlayer(Statistic statistic)
        {
            if (Actors != null)
            {
                Actors.Add(new TextBox(new Position(25, 150, 950, 75), new Position(50, 30, 0, 30), Gamestate, "Total matches:"));
                Actors.Add(new TextBox(new Position(25, 245, 950, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Total.ToString()));

                Actors.Add(new TextBox(new Position(25, 340, 950, 75), new Position(50, 30, 0, 30), Gamestate, "Won matches:"));
                Actors.Add(new TextBox(new Position(25, 435, 950, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Won.ToString()));

                Actors.Add(new TextBox(new Position(25, 530, 950, 75), new Position(50, 30, 0, 30), Gamestate, "Lost matches:"));
                Actors.Add(new TextBox(new Position(25, 625, 950, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Lost.ToString()));

                Actors.Add(new TextBox(new Position(25, 720, 950, 75), new Position(50, 30, 0, 30), Gamestate, "Drawn matches:"));
                Actors.Add(new TextBox(new Position(25, 815, 950, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Draws.ToString()));
                
                Actors.Add(new TextBox(new Position(25, 910, 950, 75), new Position(50, 30, 0, 30), Gamestate, "Win Percentage"));
                Actors.Add(new TextBox(new Position(25, 1000, 950, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Total != 0 ? ((double)statistic.Won / statistic.Total * 100).ToString("n2") + "%" : "00.00%")); ;
            }
        }

        private void GenerateStatisticsDisplayForTwoPlayers(Statistic statistic)
        {
            if (Actors != null)
            {
                Actors.Add(new TextBox(new Position(25, 150, 950, 75), new Position(50, 30, 0, 30), Gamestate, "Total matches:"));
                Actors.Add(new TextBox(new Position(25, 245, 950, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Total.ToString()));

                Actors.Add(new TextBox(new Position(25, 340, 450, 75), new Position(50, 30, 0, 30), Gamestate, "Won matches:"));
                Actors.Add(new TextBox(new Position(25, 435, 450, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Won.ToString()));

                Actors.Add(new TextBox(new Position(525, 340, 450, 75), new Position(50, 30, 0, 30), Gamestate, "Won matches:"));
                Actors.Add(new TextBox(new Position(525, 435, 450, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Lost.ToString()));

                Actors.Add(new TextBox(new Position(25, 530, 450, 75), new Position(50, 30, 0, 30), Gamestate, "Lost matches:"));
                Actors.Add(new TextBox(new Position(25, 625, 450, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Lost.ToString()));
                
                Actors.Add(new TextBox(new Position(525, 530, 450, 75), new Position(50, 30, 0, 30), Gamestate, "Lost matches:"));
                Actors.Add(new TextBox(new Position(525, 625, 450, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Won.ToString()));

                Actors.Add(new TextBox(new Position(25, 720, 950, 75), new Position(50, 30, 0, 30), Gamestate, "Drawn matches:"));
                Actors.Add(new TextBox(new Position(25, 815, 950, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Draws.ToString()));

                Actors.Add(new TextBox(new Position(25, 910, 450, 75), new Position(50, 30, 0, 30), Gamestate, "Win Percentage"));
                Actors.Add(new TextBox(new Position(25, 1000, 450, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Total != 0 ? ((double)statistic.Won / statistic.Total * 100).ToString("n2") + "%" : "00.00%"));

                Actors.Add(new TextBox(new Position(525, 910, 450, 75), new Position(50, 30, 0, 30), Gamestate, "Win Percentage"));
                Actors.Add(new TextBox(new Position(525, 1000, 450, 75), new Position(50, 30, 0, 30), Gamestate, statistic.Total != 0 ? ((double)statistic.Lost / statistic.Total * 100).ToString("n2") + "%" : "00.00%"));
            }
        }
    }
}
