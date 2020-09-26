using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    class PlayerSelectionScreen : Screen
    {
        List<ActionButton> Buttons;

        public PlayerSelectionScreen(Gamestate gamestate) : base(gamestate, ScreenType.PlayerSelectionScreen)
        {
            Buttons = new List<ActionButton>();
            int i = 0;
            foreach(int PlayerID in Gamestate.Players.Keys)
            {
                if (!gamestate.PlayersInGame.Contains(PlayerID))
                {
                    int newPlayerID = PlayerID;
                    Buttons.Add(new ActionButton(new Position(i * 30, 0, 30, 30), new Position(8, 2, 0, 20), Gamestate, PlayerID.ToString(), (MouseButtonEventArgs args) =>
                    {
                        Gamestate.AddPlayerToGame(newPlayerID);
                        MessageBus.Instance.PostEvent(MessageType.PreviousScreen, this, new EventArgs());
                    }));
                    i++;
                }
            }

            if (i == 0)
            {
                MessageBus.Instance.PostEvent(MessageType.PreviousScreen, this, new EventArgs());
            }
        }

        public override List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();
            foreach(ActionButton button in Buttons)
            {
                renderObjects.AddRange(button.GetRenderObjects());
            }
            return renderObjects;
        }
    }
}
