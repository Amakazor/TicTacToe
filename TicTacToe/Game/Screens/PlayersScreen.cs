using SFML.System;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    internal class PlayersScreen : Screen
    {
        public List<Actor> Actors { get; set; }

        public PlayersScreen(Gamestate gamestate) : base(gamestate, ScreenType.Players)
        {
            Actors = new List<Actor>();
            switch (Gamestate.PlayersInGame.Count)
            {
                case 0:
                    Actors.Add(new ScreenChangeButton(new Position(25, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Select player", ScreenType.PlayerSelectionScreen));
                    Actors.Add(new ScreenChangeButton(new Position(525, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Create player", ScreenType.NewPlayer));
                    break;

                case 1:
                    Actors.Add(new ActionButton(new Position(25, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, Gamestate.GetPlayerByPlayersInGameIndex(0).Nickname, (_) => { Gamestate.RemovePlayerFromGame(0); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    Actors.Add(new ScreenChangeButton(new Position(525, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Select second player", ScreenType.PlayerSelectionScreen));
                    break;

                case 2:
                    Actors.Add(new ActionButton(new Position(25, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, Gamestate.GetPlayerByPlayersInGameIndex(0).Nickname, (_) => { Gamestate.RemovePlayerFromGame(0); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    Actors.Add(new ActionButton(new Position(525, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, Gamestate.GetPlayerByPlayersInGameIndex(1).Nickname, (_) => { Gamestate.RemovePlayerFromGame(1); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    break;
            }

            Actors.Add(new ReturnButton(new Position(25, 875, 100, 100), Gamestate, Gamestate.TextureAtlas.TexturesDictionary[TextureType.Icon]["back"], ScreenType.MenuScreen));

            GenerateStatisticsDisplay();
        }

        public override void Dispose() { }

        public override List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();

            foreach (Actor actor in Actors)
            {
                actor.RecalculateComponentsPositions();
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
                    DisplayStatistics(GenerateStatisticsDisplayForOnePlayer(statistic));
                    break;

                case 2:
                    statistic = Gamestate.StatisticsManager.LoadPlayersStatistics(Gamestate.PlayersInGame[0], Gamestate.PlayersInGame[1]);
                    DisplayStatistics(GenerateStatisticsDisplayForTwoPlayers(statistic));
                    break;

                default:
                    return;
            }
        }

        private List<List<string>> GenerateStatisticsDisplayForOnePlayer(Statistic statistic)
        {
            return new List<List<string>>
            {
                new List<string>{ "Total matches:" },
                new List<string>{ statistic.Total.ToString() },
                new List<string>{ "Won matches:" },
                new List<string>{ statistic.Won.ToString() },
                new List<string>{ "Lost matches:" },
                new List<string>{ statistic.Lost.ToString() },
                new List<string>{ "Drawn matches:" },
                new List<string>{ statistic.Draws.ToString() },
                new List<string>{ "Win Percentage" },
                new List<string>{ statistic.Total != 0 ? ((double)statistic.Won / statistic.Total * 100).ToString("n2") + "%" : "00.00%" }
            };
        }

        private List<List<string>> GenerateStatisticsDisplayForTwoPlayers(Statistic statistic)
        {
            return new List<List<string>>
            {
                new List<string>{ "Total matches:" },
                new List<string>{ statistic.Total.ToString() },
                new List<string>{ "Won matches:","Won matches:" },
                new List<string>{ statistic.Won.ToString(), statistic.Lost.ToString() },
                new List<string>{ "Lost matches:","Lost matches:" },
                new List<string>{ statistic.Lost.ToString(), statistic.Won.ToString() },
                new List<string>{ "Drawn matches:" },
                new List<string>{ statistic.Draws.ToString() },
                new List<string>{ "Win Percentage", "Win Percentage" },
                new List<string>{ statistic.Total != 0 ? ((double)statistic.Won / statistic.Total * 100).ToString("n2") + "%" : "00.00%", statistic.Total != 0 ? ((double)statistic.Lost / statistic.Total * 100).ToString("n2") + "%" : "00.00%" }
            };
        }

        private void DisplayStatistics(List<List<string>> statisticsArray)
        {
            int totalHeight = 725;

            int startYMargin = 150;
            int boxXMargin = 25;
            int boxWidth = 1000 - boxXMargin * 2;
            int boxHeight = totalHeight / statisticsArray.Count;

            for (int i = 0; i < statisticsArray.Count; i++)
            {
                if (statisticsArray[i].Count == 1)
                {
                    Actors.Add(new TextBox(new Position(boxXMargin, startYMargin + boxHeight * i, boxWidth, boxHeight), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, statisticsArray[i][0]));
                }
                else if (statisticsArray[i].Count == 2)
                {
                    Actors.Add(new TextBox(new Position(boxXMargin, startYMargin + boxHeight * i, boxWidth / 2, boxHeight), Gamestate, new Vector2f(), 30, TextPosition.Start, TextPosition.Middle, statisticsArray[i][0]));
                    Actors.Add(new TextBox(new Position(boxXMargin * 2 + boxWidth / 2, startYMargin + boxHeight * i, boxWidth / 2, boxHeight), Gamestate, new Vector2f(), 30, TextPosition.End, TextPosition.Middle, statisticsArray[i][1]));
                }
                else throw new ArgumentException("Wrong statstics array size at index " + i, "statisticsArray");
            }
        }
    }
}