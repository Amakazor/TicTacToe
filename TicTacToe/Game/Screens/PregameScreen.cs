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
    class PregameScreen : Screen
    {
        private List<ScreenChangeButton> PlayerButtons { get; set; }
        private List<TextBox> PlayerTextBoxes { get; set; }
        private List<ActionButton> BoardSizeButtons { get; set; }
        private ScreenChangeButton StartButton { get; set; }

        public PregameScreen(Gamestate gamestate) : base(gamestate, ScreenType.Pregame)
        {
            PlayerButtons = new List<ScreenChangeButton>();
            PreparePlayerButtons();

            PlayerTextBoxes = new List<TextBox>();
            PreparePlayerTextBoxes();

            BoardSizeButtons = new List<ActionButton>();
            PrepareBoardSizeButtons();

            StartButton = new ScreenChangeButton(new Position(0, 220, 100, 100), new Position(20, 20, 0, 60), gamestate, "S", ScreenType.Game);
        }

        public override List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();
            
            foreach (ScreenChangeButton button in PlayerButtons)
            {
                renderObjects.AddRange(button.GetRenderObjects());
            }

            foreach (TextBox textBox in PlayerTextBoxes)
            {
                renderObjects.AddRange(textBox.GetRenderObjects());
            }

            if (Gamestate.BoardSize == 0)
            {
                foreach (ActionButton button in BoardSizeButtons)
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
                    PlayerButtons.Add(new ScreenChangeButton(new Position(0, playersToSelect * 110, 100, 100), new Position(30, 10, 0, 60), Gamestate, "P", ScreenType.PlayerSelectionScreen));
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
                        (new Position(0, playerIndex * 110, 300, 100),
                        new Position(30, 10, 0, 60),
                        Gamestate,
                        Gamestate.GetPlayerByPlayersInGameIndex(playerIndex).Nickname));
                }
            }
        }

        private void PrepareBoardSizeButtons()
        {
            for (int size = 2; size < 8; size++)
            {
                int newSize = size;
                BoardSizeButtons.Add(new ActionButton(
                    new Position(((size - 2) % 3) * 40, 220 + (int)MathF.Floor((size - 2) / 3) * 40, 30, 30),
                    new Position(8, 2, 0, 20),
                    Gamestate,
                    size.ToString(),
                    (MouseButtonEventArgs args) =>
                        {
                            Gamestate.BoardSize = newSize;
                            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
                        }));
            }
        }
        public override void Dispose(){}
    }
}
