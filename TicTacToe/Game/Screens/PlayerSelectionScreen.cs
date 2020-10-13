using SFML.System;
using SFML.Window;
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
    internal class PlayerSelectionScreen : Screen
    {
        private ScreenChangeButton CreatePlayerButton;
        private List<PlayerData> Players;
        private List<DeletePlayerButton> DeletePlayers;
        private List<ChangePageButton> PageButtons;

        private int CurrentPage;

        private ReturnButton ReturnButton { get; set; }

        public PlayerSelectionScreen(Gamestate gamestate) : base(gamestate, ScreenType.PlayerSelectionScreen)
        {
            Gamestate.NewPlayer = null;

            CurrentPage = 0;

            CreatePlayerButton = new ScreenChangeButton(new Position(25, 25, 950, 100), Gamestate, new Vector2f(0, 0), 30, TextPosition.Middle, TextPosition.Middle, "Create new player", ScreenType.NewPlayer);

            GeneratePlayerButtons();

            PageButtons = new List<ChangePageButton>
            {
                new ChangePageButton(new Position(550, 875, 200, 100), Gamestate, new Vector2f(0, 0), 30, TextPosition.Middle, TextPosition.Middle, "Prev Page", ChangePage, false),
                new ChangePageButton(new Position(775, 875, 200, 100), Gamestate, new Vector2f(0, 0), 30, TextPosition.Middle, TextPosition.Middle, "Next Page", ChangePage, true)
            };
            RefreshPageButtonsState();

            ReturnButton = new ReturnButton(new Position(25, 875, 100, 100), Gamestate, Gamestate.TextureAtlas.TexturesDictionary[TextureType.Icon]["back"], Gamestate.PreviousScreen);
        }

        public override void Dispose() {}

        public override List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>(); 

            CreatePlayerButton.RecalculateComponentsPositions();
            renderObjects.AddRange(CreatePlayerButton.GetRenderObjects());

            CheckForPageCorrectness();

            GeneratePlayerButtons();
            foreach (Actor actor in Players)
            {
                actor.RecalculateComponentsPositions();
                renderObjects.AddRange(actor.GetRenderObjects());
            }
            
            if (Gamestate.PreviousScreen == ScreenType.MenuScreen)
            {
                foreach (Actor actor in DeletePlayers)
                {
                    actor.RecalculateComponentsPositions();
                    renderObjects.AddRange(actor.GetRenderObjects());
                }
            }

            foreach (Actor actor in PageButtons)
            {
                actor.RecalculateComponentsPositions();
                renderObjects.AddRange(actor.GetRenderObjects());
            }

            ReturnButton.RecalculateComponentsPositions();
            renderObjects.AddRange(ReturnButton.GetRenderObjects());

            return renderObjects;
        }

        private void GeneratePlayerButtons()
        {
            Players = new List<PlayerData>();
            DeletePlayers = new List<DeletePlayerButton>();

            List<int> ids = Gamestate.PlayersManager.Players.Keys.ToList();

            int beginId = CurrentPage * 5;
            int endId = Math.Min(ids.Count, beginId + 5);

            for (int i = beginId; i < endId; i++)
            {
                Players.Add(new PlayerData(new Position(25, 25 + (25 + 100) * (i - beginId + 1), Gamestate.PreviousScreen == ScreenType.MenuScreen ? 825 : 950, 100), Gamestate, ids[i]));
                DeletePlayers.Add(new DeletePlayerButton(new Position(875, 25 + (25 + 100) * (i - beginId + 1), 100, 100), Gamestate, Gamestate.TextureAtlas.TexturesDictionary[TextureType.Icon]["delete"], ids[i]));
                
                if (ids[i] < 3)
                {
                    DeletePlayers.Last().ButtonState = ButtonState.Inactive;
                }

                if (Gamestate.PlayersInGame.Contains(ids[i]))
                {
                    Players.Last().setButtonState(ButtonState.Inactive);
                }
            }
        }

        private void RefreshPageButtonsState()
        {
            PageButtons[0].ButtonState = (CurrentPage == 0) ? ButtonState.Inactive : ButtonState.Active;

            PageButtons[1].ButtonState = (CurrentPage * 5 + 5 >= Gamestate.PlayersManager.Players.Keys.ToList().Count()) ? ButtonState.Inactive : ButtonState.Active;
        }

        private void ChangePage(MouseButtonEventArgs args, bool changeToNext)
        {
            CurrentPage = (changeToNext) ? CurrentPage + 1 : CurrentPage - 1;

            RefreshPageButtonsState();
            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        private void CheckForPageCorrectness()
        {
            if (CurrentPage != 0 && (CurrentPage * 5 >= Gamestate.PlayersManager.Players.Keys.ToList().Count()))
            {
                ChangePage(new MouseButtonEventArgs(new MouseButtonEvent()), false);
            }
        }
    }
}