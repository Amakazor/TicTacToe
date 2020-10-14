using SFML.System;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    internal class PlayerData : Actor
    {
        private SelectPlayerButton SelectPlayerButton { get; set; }
        private List<Text> Texts { get; set; }

        public PlayerData(Position position, Gamestate gamestate, PlayersManager playersManager, StatisticsManager statisticsManager, int playerId) : base(position, gamestate)
        {
            if (playersManager.Players.ContainsKey(playerId))
            {
                SelectPlayerButton = new SelectPlayerButton(Position, Gamestate, playersManager, playerId, Gamestate.PreviousScreen == ScreenType.MenuScreen ? ScreenType.Statistics : Gamestate.PreviousScreen);

                string nickname = playersManager.Players[playerId].Nickname;

                Statistic statistic = statisticsManager.LoadPlayersStatistics(playerId);
                string total = statistic.Total.ToString();
                string won = statistic.Won.ToString();

                Texts = new List<Text>
                {
                    new Text(new Position(Position.X, Position.Y, Position.Width / 2, Position.Height), Gamestate, new Vector2f(10, 0), 30, TextPosition.Start, TextPosition.Middle, nickname),

                    new Text(new Position(Position.X + Position.Width / 2, Position.Y, Position.Width / 4, Position.Height / 2), Gamestate, new Vector2f(0, 0), 30, TextPosition.Start, TextPosition.Middle, "Total:"),
                    new Text(new Position(Position.X + Position.Width / 2, Position.Y + Position.Height / 2, Position.Width / 4, Position.Height / 2), Gamestate, new Vector2f(0, 0), 30, TextPosition.Start, TextPosition.Middle, total),
                    new Text(new Position(Position.X + Position.Width / 2 + Position.Width / 4, Position.Y, Position.Width / 4, Position.Height / 2), Gamestate, new Vector2f(0, 0), 30, TextPosition.Start, TextPosition.Middle, "Won:"),
                    new Text(new Position(Position.X + Position.Width / 2 + Position.Width / 4, Position.Y + Position.Height / 2, Position.Width / 4, Position.Height / 2), Gamestate, new Vector2f(0, 0), 30, TextPosition.Start, TextPosition.Middle, won)
                };

                RecalculateComponentsPositions();
            }
            else throw new ArgumentException("Invalid ID", "playerId");
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();

            renderObjects.AddRange(SelectPlayerButton.GetRenderObjects());

            foreach (Text text in Texts)
            {
                renderObjects.AddRange(text.GetRenderObjects());
            }

            return renderObjects;
        }

        public override void RecalculateComponentsPositions()
        {
            SelectPlayerButton.RecalculateComponentsPositions();

            foreach (Text text in Texts)
            {
                text.RecalculateComponentsPositions();
            }
        }

        public void setButtonState(ButtonState buttonState)
        {
            SelectPlayerButton.ButtonState = buttonState;
        }
    }
}