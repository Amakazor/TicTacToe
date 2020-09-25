using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    class PregameScreen : Screen, IScreen
    {
        private List<Button> PlayerButtons { get; set; }
        private List<TextBox> PlayerTextBoxes { get; set; }
        private List<ButtonInt> BoardSizeButtons { get; set; }
        private Button StartButton { get; set; }

        public PregameScreen(Gamestate gamestate) : base(gamestate, EScreens.Pregame)
        {
            PlayerButtons = new List<Button>();
            PreparePlayerButtons();

            PlayerTextBoxes = new List<TextBox>();
            PreparePlayerTextBoxes();

            BoardSizeButtons = new List<ButtonInt>();
            PrepareBoardSizeButtons();

            StartButton = new Button(new Position(0, 220, 100, 100), Gamestate, (_) =>
            {
                MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = EScreens.Game });
            }, "S");
        }

        public EScreens GetEScreen()
        {
            return EScreen;
        }

        public List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();
            
            foreach (Button button in PlayerButtons)
            {
                renderObjects.AddRange(button.GetRenderObjects());
            }

            foreach (TextBox textBox in PlayerTextBoxes)
            {
                renderObjects.AddRange(textBox.GetRenderObjects());
            }

            if (Gamestate.BoardSize == 0)
            {
                foreach (ButtonInt button in BoardSizeButtons)
                {
                    renderObjects.AddRange(button.GetRenderObjects());
                }
            }

            if (Gamestate.CanStartGame())
            {
                renderObjects.AddRange(StartButton.GetRenderObjects());
            }

            return renderObjects;
        }

        private void PreparePlayerButtons()
        {
            int selectedPlayersAmount = Gamestate.PlayersInGame.Count;

            if (selectedPlayersAmount < 2)
            {
                for (int playersToSelect = selectedPlayersAmount; playersToSelect < 2; playersToSelect++)
                {
                    PlayerButtons.Add(new Button(new Position(0, playersToSelect * 110, 100, 100), Gamestate, (MouseButtonEventArgs args) =>
                    {
                        MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = EScreens.PlayerSelectionScreen });
                    }, "P"));
                }
            }
        }

        private void PreparePlayerTextBoxes()
        {
            int selectedPlayersAmount = Gamestate.PlayersInGame.Count;

            if (selectedPlayersAmount > 0)
            {
                for (int playerIndex = 0; playerIndex < selectedPlayersAmount; playerIndex++)
                {
                    PlayerTextBoxes.Add(new TextBox
                        (new Position(0, playerIndex * 110, 100, 100),
                        new Position(10, 10 + playerIndex * 110, 0, 16),
                        Gamestate,
                        Gamestate.GetPlayerByPlayersInGameIndex(playerIndex).Nickname));
                }
            }
        }

        private void PrepareBoardSizeButtons()
        {
            for (int size = 2; size < 8; size++)
            {
                BoardSizeButtons.Add(new ButtonInt(new Position(((size-2) % 3) * 40, 220 + (int)MathF.Floor((size-2) / 3) * 40, 30, 30), Gamestate, (MouseButtonEventArgs args, int newSize) =>
                {
                    Gamestate.BoardSize = newSize;
                    MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
                }, size, size.ToString()));
            }
        }
    }
}
