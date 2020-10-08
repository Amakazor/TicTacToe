using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    internal class PlayerSelectionScreen : Screen
    {
        private List<ActionButton> Buttons;

        public PlayerSelectionScreen(Gamestate gamestate) : base(gamestate, ScreenType.PlayerSelectionScreen)
        {
            Buttons = new List<ActionButton>();
            int i = 0;
            foreach (int PlayerID in Gamestate.PlayersManager.Players.Keys)
            {
                if (!gamestate.PlayersInGame.Contains(PlayerID))
                {
                    int newPlayerID = PlayerID;
                    Buttons.Add(new ActionButton(new Position(i * 30, 0, 30, 30), Gamestate, new Vector2f(), 20, TextPosition.Middle, TextPosition.Middle, PlayerID.ToString(), (MouseButtonEventArgs args) =>
                    {
                        Gamestate.AddPlayerToGame(newPlayerID);
                        MessageBus.Instance.PostEvent(MessageType.PreviousScreen, this, new EventArgs());
                    }));
                    i++;
                }
            }

            if (i == 0) MessageBus.Instance.PostEvent(MessageType.PreviousScreen, this, new EventArgs());
        }

        public override void Dispose() {}

        public override List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();
            foreach (ActionButton button in Buttons)
            {
                button.RecalculateComponentsPositions();
                renderObjects.AddRange(button.GetRenderObjects());
            }
            return renderObjects;
        }
    }
}