﻿using SFML.System;
using SFML.Window;
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
    internal class ResultsScreen : Screen
    {
        private TextBox TextBox;
        private List<TextButton> Buttons;

        public ResultsScreen(Gamestate gamestate) : base(gamestate, ScreenType.Results)
        {
            SaveStatistics();

            TextBox = new TextBox(new Position(200, 0, 600, 100), Gamestate, new Vector2f(), 40, TextPosition.Middle, TextPosition.Middle, GetResultsText());

            Buttons = new List<TextButton>();
            Buttons.Add(new ActionButton(new Position(200, 220, 600, 100), Gamestate, new Vector2f(), 40, TextPosition.Middle, TextPosition.Middle, "Rematch", (MouseButtonEventArgs args) => { Gamestate.ChangePlayer(); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.Game }); }));
            Buttons.Add(new ActionButton(new Position(200, 440, 600, 100), Gamestate, new Vector2f(), 40, TextPosition.Middle, TextPosition.Middle, "New Game", (MouseButtonEventArgs args) => { Gamestate.Clear(); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.Pregame }); }));
            Buttons.Add(new ScreenChangeButton(new Position(200, 640, 600, 100), Gamestate, new Vector2f(), 40, TextPosition.Middle, TextPosition.Middle, "Return to Main Menu", ScreenType.MenuScreen));
        }

        public override void Dispose() { }

        public override List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();

            TextBox.RecalculateComponentsPositions();
            renderObjects.AddRange(TextBox.GetRenderObjects());

            foreach (TextButton button in Buttons)
            {
                button.RecalculateComponentsPositions();
                renderObjects.AddRange(button.GetRenderObjects());
            }

            return renderObjects;
        }

        private string GetResultsText()
        {
            switch (Gamestate.boardstate)
            {
                case Boardstate.Draw:
                    return "DRAW!";

                case Boardstate.Won:
                    return Gamestate.GetCurrentPlayer().Nickname + " WON!";

                default:
                    throw new Exception("Invalid board state");
            }
        }

        private void SaveStatistics()
        {
            switch (Gamestate.boardstate)
            {
                case Boardstate.Draw:
                    Gamestate.StatisticsManager.AddAndSaveStatistic(Gamestate.PlayersInGame[0], Gamestate.PlayersInGame[1], 0);
                    break;

                case Boardstate.Won:
                    Gamestate.StatisticsManager.AddAndSaveStatistic(Gamestate.PlayersInGame[0], Gamestate.PlayersInGame[1], Gamestate.CurrentPlayer);
                    break;

                default:
                    throw new Exception("Invalid board state");
            }
        }
    }
}