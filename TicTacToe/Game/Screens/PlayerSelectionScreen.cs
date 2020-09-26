using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    class PlayerSelectionScreen : Screen
    {
        List<ButtonInt> Buttons;

        public PlayerSelectionScreen(Gamestate gamestate) : base(gamestate, EScreens.PlayerSelectionScreen)
        {
            Buttons = new List<ButtonInt>();
            int i = 0;
            foreach(int PlayerID in Gamestate.Players.Keys)
            {
                if (!gamestate.PlayersInGame.Contains(PlayerID))
                {
                    Buttons.Add(new ButtonInt(new Position(i * 30, 0, 30, 30), Gamestate, (MouseButtonEventArgs args, int playerID) => {
                        Gamestate.AddPlayerToGame(playerID);
                        MessageBus.Instance.PostEvent(MessageType.PreviousScreen, this, new EventArgs());
                    }, PlayerID, PlayerID.ToString()));
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
            foreach(ButtonInt button in Buttons)
            {
                renderObjects.AddRange(button.GetRenderObjects());
            }
            return renderObjects;
        }
    }
}
