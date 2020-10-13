using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    class PlayerData : Actor
    {
        private SelectPlayerButton SelectPlayerButton { get; set; }
        private List<TextBox> TextBoxes { get; set; }

        public PlayerData(Position position, Gamestate gamestate, int playerId) : base(position, gamestate)
        {
            if (Gamestate.PlayersManager.Players.ContainsKey(playerId))
            {
                SelectPlayerButton = new SelectPlayerButton(Position, Gamestate, playerId, Gamestate.PreviousScreen == ScreenType.MenuScreen ? ScreenType.Statistics : Gamestate.PreviousScreen);

                string nickname = Gamestate.PlayersManager.Players[playerId].Nickname;

                Statistic statistic = Gamestate.StatisticsManager.LoadPlayersStatistics(playerId);
                string total = statistic.Total.ToString();
                string won = statistic.Won.ToString();

                TextBoxes = new List<TextBox>
                {
                    new TextBox(new Position(Position.X, Position.Y, Position.Width / 2, Position.Height), Gamestate, new Vector2f(10, 0), 30, TextPosition.Start, TextPosition.Middle, nickname),

                    new TextBox(new Position(Position.X + Position.Width / 2, Position.Y, Position.Width / 4, Position.Height / 2), Gamestate, new Vector2f(0, 0), 30, TextPosition.Start, TextPosition.Middle, "Total:"),
                    new TextBox(new Position(Position.X + Position.Width / 2, Position.Y + Position.Height / 2, Position.Width / 4, Position.Height / 2), Gamestate, new Vector2f(0, 0), 30, TextPosition.Start, TextPosition.Middle, total),
                    new TextBox(new Position(Position.X + Position.Width / 2 + Position.Width / 4, Position.Y, Position.Width / 4, Position.Height / 2), Gamestate, new Vector2f(0, 0), 30, TextPosition.Start, TextPosition.Middle, "Won:"),
                    new TextBox(new Position(Position.X + Position.Width / 2 + Position.Width / 4, Position.Y + Position.Height / 2, Position.Width / 4, Position.Height / 2), Gamestate, new Vector2f(0, 0), 30, TextPosition.Start, TextPosition.Middle, won)
                };

                RecalculateComponentsPositions();
            }
            else throw new ArgumentException("Invalid ID", "playerId");
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();

            renderObjects.AddRange(SelectPlayerButton.GetRenderObjects());

            foreach (TextBox textBox in TextBoxes)
            {
                renderObjects.AddRange(textBox.GetRenderObjects());
            }

            return renderObjects;
        }

        public override void RecalculateComponentsPositions()
        {
            SelectPlayerButton.RecalculateComponentsPositions();

            foreach ( TextBox textBox in TextBoxes)
            {
                textBox.RecalculateComponentsPositions();
            }
        }

        public void setButtonState(ButtonState buttonState)
        {
            SelectPlayerButton.ButtonState = buttonState;
        }
    }
}
