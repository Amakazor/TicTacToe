using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Actors.Inputs;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    internal class NewPlayerScreen : Screen
    {
        private List<Actor> Actors;
        private ActionButton CreatePlayerButton;
        private List<SelectSymbolButton> SymbolButtons;
        private ReturnButton ReturnButton;

        public NewPlayerScreen(Gamestate gamestate, PlayersManager playersManager, TextureManager textureManager) : base(gamestate, playersManager, ScreenType.NewPlayer)
        {
            PlayersManager.NewPlayer = new Player("", null, new Color(127, 127, 127), Gamestate, textureManager);

            Actors = new List<Actor>();
            Actors.Add(new TextInput(new Position(25, 25, 950, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Enter players name (4 to 10 characters):", HandleNameChange, 0));
            Actors.Add(new RangeInput(new Position(50, 140, 900, 50), Gamestate, PlayersManager, PlayersManager.NewPlayer.SymbolData.color.R, byte.MinValue, byte.MaxValue, Color.Red, HandleColorChange, 1));
            Actors.Add(new RangeInput(new Position(50, 205, 900, 50), Gamestate, PlayersManager, PlayersManager.NewPlayer.SymbolData.color.G, byte.MinValue, byte.MaxValue, Color.Green, HandleColorChange, 2));
            Actors.Add(new RangeInput(new Position(50, 270, 900, 50), Gamestate, PlayersManager, PlayersManager.NewPlayer.SymbolData.color.B, byte.MinValue, byte.MaxValue, Color.Blue, HandleColorChange, 3));

            if (textureManager.TexturesDictionary.ContainsKey(TextureType.Symbol))
            {
                int size = 200;
                int marginOuter = 25;
                int marginInner = ((1000 - 2 * marginOuter) - (size * 4)) / 3;

                int i = 0;
                SymbolButtons = new List<SelectSymbolButton>();
                foreach (KeyValuePair<string, Texture> rawSymbol in textureManager.TexturesDictionary[TextureType.Symbol])
                {
                    Position curentPosition = new Position(marginOuter + i % 4 * (size + marginInner), 335 + (i / 4) * (size + marginInner), size, size);
                    SymbolButtons.Add(new SelectSymbolButton(curentPosition, Gamestate, PlayersManager, rawSymbol.Value, 0.8F, PlayersManager.NewPlayer.SymbolData.color));
                    i++;
                }
            }
            else throw new Exception("Symbols have not been loaded");

            CreatePlayerButton = new ActionButton(new Position(150, 875, 825, 100), Gamestate, new Vector2f(), 40, TextPosition.Middle, TextPosition.Middle, "Save Player", SaveNewPlayer);
            CreatePlayerButton.ButtonState = ButtonState.Inactive;

            ReturnButton = new ReturnButton(new Position(25, 875, 100, 100), Gamestate, textureManager.TexturesDictionary[TextureType.Icon]["back"], Gamestate.PreviousScreen, Gamestate.SecondPreviousScreen);
        }

        public override void Dispose()
        {
            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        public override List<IRenderObject> GetRenderData()
        {
            ChangeButtonStates();

            List<IRenderObject> renderObjects = new List<IRenderObject>();

            foreach (Actor actor in Actors)
            {
                actor.RecalculateComponentsPositions();
                renderObjects.AddRange(actor.GetRenderObjects());
            }

            foreach (Actor actor in SymbolButtons)
            {
                actor.RecalculateComponentsPositions();
                renderObjects.AddRange(actor.GetRenderObjects());
            }

            if (PlayersManager.ValidateNewPlayer())
            {
                CreatePlayerButton.ButtonState = ButtonState.Active;
            }
            else
            {
                CreatePlayerButton.ButtonState = ButtonState.Inactive;
            }

            CreatePlayerButton.RecalculateComponentsPositions();
            renderObjects.AddRange(CreatePlayerButton.GetRenderObjects());

            ReturnButton.RecalculateComponentsPositions();
            renderObjects.AddRange(ReturnButton.GetRenderObjects());

            return renderObjects;
        }

        private void HandleNameChange(TextInput input, TextEventArgs textEventArgs)
        {
            if (textEventArgs.Unicode == "\b" && PlayersManager.NewPlayer.Nickname.Length != 0)
            {
                PlayersManager.NewPlayer.Nickname = PlayersManager.NewPlayer.Nickname.Remove(PlayersManager.NewPlayer.Nickname.Length - 1);
            }
            else if (textEventArgs.Unicode != "\x1B" && textEventArgs.Unicode != "\t" && textEventArgs.Unicode != "\b" &&(PlayersManager.NewPlayer.Nickname == null || PlayersManager.NewPlayer.Nickname.Length < 10))
            {
                PlayersManager.NewPlayer.Nickname += textEventArgs.Unicode;
            }

            input.ChangeText(PlayersManager.NewPlayer.Nickname);
        }

        private void HandleColorChange(RangeInput input, int value)
        {
            Color currentColor = PlayersManager.NewPlayer.SymbolData.color;
            switch (input.Id)
            {
                case 1:
                    currentColor = new Color((byte)value, currentColor.G, currentColor.B);
                    break;

                case 2:
                    currentColor = new Color(currentColor.R, (byte)value, currentColor.B);
                    break;

                case 3:
                    currentColor = new Color(currentColor.R, currentColor.G, (byte)value);
                    break;
            }

            SymbolButtons.ForEach((button) => { button.ChangeColor(currentColor); });

            PlayersManager.NewPlayer.SymbolData.color = currentColor;
            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        private void ChangeButtonStates()
        {
            foreach (SelectSymbolButton button in SymbolButtons)
            {
                if (button.IconTexture == PlayersManager.NewPlayer.SymbolData.texture)
                {
                    button.ButtonState = ButtonState.Focused;
                }
                else
                {
                    button.ButtonState = ButtonState.Active;
                }
            }
        }

        private void SaveNewPlayer(MouseButtonEventArgs mouseButtonEventArgs)
        {
            int id = PlayersManager.SaveNewPlayer();

            ScreenType previousScreen = Gamestate.PreviousScreen;
            Gamestate.PreviousScreen = Gamestate.SecondPreviousScreen;
            MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = previousScreen, ChangePreviousScreen = false });
        }
    }
}