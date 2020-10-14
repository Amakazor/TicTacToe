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
    internal class PregameScreen : Screen
    {
        private List<TextButton> PlayerButtons { get; set; }
        private Text Text { get; set; }
        private List<ActionButton> BoardSizeButtons { get; set; }
        private ScreenChangeButton StartButton { get; set; }
        private ReturnButton ReturnButton { get; set; }

        public PregameScreen(Gamestate gamestate, PlayersManager playersManager, TextureManager textureManager) : base(gamestate, playersManager, ScreenType.Pregame)
        {
            PlayersManager.NewPlayer = null;

            PlayerButtons = new List<TextButton>();
            PreparePlayerButtons();

            Text = new Text(new Position(25, 150, 950, 100), Gamestate, new Vector2f(), 50, TextPosition.Middle, TextPosition.Middle, "Select board size:");

            BoardSizeButtons = new List<ActionButton>();
            PrepareBoardSizeButtons();

            StartButton = new ScreenChangeButton(new Position(25, BoardSizeButtons.Last().Position.Height + BoardSizeButtons.Last().Position.Y + 25, 950, 150), Gamestate, new Vector2f(), 60, TextPosition.Middle, TextPosition.Middle, "Start!", ScreenType.Game);
            StartButton.ButtonState = ButtonState.Inactive;

            ReturnButton = new ReturnButton(new Position(25, 875, 100, 100), Gamestate, textureManager.TexturesDictionary[TextureType.Icon]["back"], ScreenType.MenuScreen);
        }

        public override List<IRenderObject> GetRenderData()
        {
            ChangeButtonStates();

            List<IRenderObject> renderObjects = new List<IRenderObject>();

            foreach (TextButton button in PlayerButtons)
            {
                button.RecalculateComponentsPositions();
                renderObjects.AddRange(button.GetRenderObjects());
            }

            foreach (ActionButton button in BoardSizeButtons)
            {
                button.RecalculateComponentsPositions();
                renderObjects.AddRange(button.GetRenderObjects());
            }

            Text.RecalculateComponentsPositions();
            renderObjects.AddRange(Text.GetRenderObjects());

            if (Gamestate.CanStartGame())
            {
                StartButton.ButtonState = ButtonState.Active;
            }
            else
            {
                StartButton.ButtonState = ButtonState.Inactive;
            }

            StartButton.RecalculateComponentsPositions();
            renderObjects.AddRange(StartButton.GetRenderObjects());

            ReturnButton.RecalculateComponentsPositions();
            renderObjects.AddRange(ReturnButton.GetRenderObjects());

            return renderObjects;
        }

        private void PreparePlayerButtons()
        {
            switch (PlayersManager.PlayersInGame.Count)
            {
                case 0:
                    PlayerButtons.Add(new ScreenChangeButton(new Position(25, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Select player", ScreenType.PlayerSelectionScreen));
                    PlayerButtons.Add(new ScreenChangeButton(new Position(525, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Select second player", ScreenType.PlayerSelectionScreen));
                    PlayerButtons.Last().ButtonState = ButtonState.Inactive;
                    break;

                case 1:
                    PlayerButtons.Add(new ActionButton(new Position(25, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, PlayersManager.GetPlayerByPlayersInGameIndex(0).Nickname, (_) => { PlayersManager.RemovePlayerFromGame(0); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    PlayerButtons.Add(new ScreenChangeButton(new Position(525, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Select second player", ScreenType.PlayerSelectionScreen));
                    break;

                case 2:
                    PlayerButtons.Add(new ActionButton(new Position(25, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, PlayersManager.GetPlayerByPlayersInGameIndex(0).Nickname, (_) => { PlayersManager.RemovePlayerFromGame(0); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    PlayerButtons.Add(new ActionButton(new Position(525, 25, 450, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, PlayersManager.GetPlayerByPlayersInGameIndex(1).Nickname, (_) => { PlayersManager.RemovePlayerFromGame(1); MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.PlayerSelectionScreen }); }));
                    break;
            }
        }

        private void PrepareBoardSizeButtons()
        {
            int totalWidth = 950;
            int marginLeft = 25;
            int spacing = 30;

            int minValue = 3;
            int maxValue = 9;

            int count = maxValue - minValue;

            int width = (totalWidth - ((count - 1) * spacing)) / count;

            for (int size = minValue; size < maxValue; size++)
            {
                int newSize = size;

                BoardSizeButtons.Add(new ActionButton(
                    new Position(marginLeft + (width + spacing) * (size - minValue), 275, width, width),
                    Gamestate, new Vector2f(), (int)(width * 0.5F), TextPosition.Middle, TextPosition.Middle, size.ToString(), (_) => { SetBoardSize(newSize); }));
            }
        }

        private void SetBoardSize(int newSize)
        {
            Gamestate.BoardSize = newSize;
            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        private void ChangeButtonStates()
        {
            foreach (ActionButton button in BoardSizeButtons)
            {
                if (button.GetText() == Gamestate.BoardSize.ToString())
                {
                    button.ButtonState = ButtonState.Focused;
                }
                else
                {
                    button.ButtonState = ButtonState.Active;
                }
            }
        }

        public override void Dispose()
        {
        }
    }
}