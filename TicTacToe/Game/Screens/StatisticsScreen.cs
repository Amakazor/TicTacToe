using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    internal class StatisticsScreen : Screen
    {
        public List<Actor> Actors { get; set; }
        private StatisticsManager StatisticsManager;

        public StatisticsScreen(Gamestate gamestate, PlayersManager playersManager, TextureManager textureManager, StatisticsManager statisticsManager) : base(gamestate, playersManager, ScreenType.Statistics)
        {
            StatisticsManager = statisticsManager;

            Actors = new List<Actor>();
            switch (PlayersManager.PlayersInGame.Count)
            {
                case 0:
                    Actors.Add(new ScreenChangeButton(new Position(25, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Select player", ScreenType.PlayerSelectionScreen));
                    Actors.Add(new ScreenChangeButton(new Position(525, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Select second player", ScreenType.PlayerSelectionScreen));
                    ((ScreenChangeButton)Actors.Last()).ButtonState = ButtonState.Inactive;
                    break;

                case 1:
                    Actors.Add(new ActionButton(new Position(25, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, PlayersManager.GetPlayerByPlayersInGameIndex(0).Nickname, (_) => { PlayersManager.RemovePlayerFromGame(0); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    Actors.Add(new ScreenChangeButton(new Position(525, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Select second player", ScreenType.PlayerSelectionScreen));
                    break;

                case 2:
                    Actors.Add(new ActionButton(new Position(25, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, PlayersManager.GetPlayerByPlayersInGameIndex(0).Nickname, (_) => { PlayersManager.RemovePlayerFromGame(0); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    Actors.Add(new ActionButton(new Position(525, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, PlayersManager.GetPlayerByPlayersInGameIndex(1).Nickname, (_) => { PlayersManager.RemovePlayerFromGame(1); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    break;
            }

            Actors.Add(new ReturnButton(new Position(25, 875, 100, 100), Gamestate, textureManager.TexturesDictionary[TextureType.Icon]["back"], ScreenType.MenuScreen));

            GenerateStatisticsDisplay();
        }

        public override void Dispose()
        {
        }

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

            switch (PlayersManager.PlayersInGame.Count)
            {
                case 0:
                    DisplayStatistics(GenerateTop());
                    break;
                case 1:
                    statistic = StatisticsManager.LoadPlayersStatistics(PlayersManager.PlayersInGame[0]);
                    DisplayStatistics(GenerateStatisticsDisplayForOnePlayer(statistic));
                    break;

                case 2:
                    statistic = StatisticsManager.LoadPlayersStatistics(PlayersManager.PlayersInGame[0], PlayersManager.PlayersInGame[1]);
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

        private List<List<string>> GenerateTop()
        {
            Dictionary<int, double> statistics = StatisticsManager.LoadPlayersStatistics(PlayersManager.Players.Keys.ToList());
            List<List<string>> strings = new List<List<string>>
            {
                new List<string> {"TOP 10 PLAYERS (Win percentage)"}
            };

            int i = 0;
            foreach (KeyValuePair<int, double> statistic in statistics.OrderBy(d => d.Value).Reverse().ToList())
            {
                if (i < 10 && PlayersManager.Players.ContainsKey(statistic.Key))
                {
                    strings.Add(new List<string>
                    {
                        PlayersManager.Players[statistic.Key].Nickname + " :" + (statistic.Value > 0.0D ? statistic.Value.ToString("n2") + "%" : "00.00%")
                    });
                }
                i++;
            }

            return strings;
        }

        private void DisplayStatistics(List<List<string>> statisticsArray)
        {
            int totalHeight = 650;

            int startYMargin = 150;
            int boxXMargin = 25;
            int boxWidth = 1000 - boxXMargin * 2;
            int boxHeight = totalHeight / statisticsArray.Count;
            Vector2f textMargin = new Vector2f(10, 0);
            int fontSize = 30;

            for (int i = 0; i < statisticsArray.Count; i++)
            {
                if (statisticsArray[i].Count == 1)
                {
                    Actors.Add(new Text(new Position(boxXMargin, startYMargin + boxHeight * i, boxWidth, boxHeight), Gamestate, textMargin, fontSize, TextPosition.Middle, TextPosition.Middle, statisticsArray[i][0]));
                }
                else if (statisticsArray[i].Count == 2)
                {
                    Actors.Add(new Text(new Position(boxXMargin, startYMargin + boxHeight * i, boxWidth / 2, boxHeight), Gamestate, textMargin, fontSize, TextPosition.Start, TextPosition.Middle, statisticsArray[i][0]));
                    Actors.Add(new Text(new Position(boxXMargin * 2 + boxWidth / 2, startYMargin + boxHeight * i, (boxWidth - (boxXMargin * 2)) / 2, boxHeight), Gamestate, textMargin, fontSize, TextPosition.End, TextPosition.Middle, statisticsArray[i][1]));
                }
                else throw new ArgumentException("Wrong statstics array size at index " + i, "statisticsArray");
            }
        }
    }
}