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

        public NewPlayerScreen(Gamestate gamestate) : base(gamestate, ScreenType.NewPlayer)
        {
            Gamestate.NewPlayer = new Player("", null, new Color(127, 127, 127), Gamestate);

            Actors = new List<Actor>();
            Actors.Add(new TextInput(new Position(25, 25, 950, 100), Gamestate, new Vector2f(), 30, TextPosition.Middle, TextPosition.Middle, "Enter players name (4 to 10 characters):", HandleNameChange, 0));
            Actors.Add(new RangeInput(new Position(50, 140, 900, 50), Gamestate, Gamestate.NewPlayer.SymbolData.color.R, byte.MinValue, byte.MaxValue, Color.Red, HandleColorChange, 1));
            Actors.Add(new RangeInput(new Position(50, 205, 900, 50), Gamestate, Gamestate.NewPlayer.SymbolData.color.G, byte.MinValue, byte.MaxValue, Color.Green, HandleColorChange, 2));
            Actors.Add(new RangeInput(new Position(50, 270, 900, 50), Gamestate, Gamestate.NewPlayer.SymbolData.color.B, byte.MinValue, byte.MaxValue, Color.Blue, HandleColorChange, 3));

            if (Gamestate.TextureAtlas.TexturesDictionary.ContainsKey(TextureType.Symbol))
            {
                int size = 200;
                int marginOuter = 25;
                int marginInner = ((1000 - 2 * marginOuter) - (size * 4)) / 3;

                int i = 0;
                SymbolButtons = new List<SelectSymbolButton>();
                foreach (KeyValuePair<string, Texture> rawSymbol in Gamestate.TextureAtlas.TexturesDictionary[TextureType.Symbol])
                {
                    Position curentPosition = new Position(marginOuter + i % 4 * (size + marginInner), 335 + (i / 4) * (size + marginInner), size, size);
                    SymbolButtons.Add(new SelectSymbolButton(curentPosition, Gamestate, rawSymbol.Value, 0.8F, Gamestate.NewPlayer.SymbolData.color));
                    i++;
                }
            }
            else throw new Exception();

            CreatePlayerButton = new ActionButton(new Position(25, 845, 950, 130), Gamestate, new Vector2f(), 40, TextPosition.Middle, TextPosition.Middle, "Save Player", SaveNewPlayer);
            CreatePlayerButton.ButtonState = ButtonState.Inactive;
        }

        public override void Dispose() { }

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

            if (Gamestate.ValidateNewPlayer())
            {
                CreatePlayerButton.ButtonState = ButtonState.Active;
            }
            else
            {
                CreatePlayerButton.ButtonState = ButtonState.Inactive;
            }

            CreatePlayerButton.RecalculateComponentsPositions();
            renderObjects.AddRange(CreatePlayerButton.GetRenderObjects());

            return renderObjects;
        }

        private void HandleNameChange(TextInput input, TextEventArgs textEventArgs)
        {
            if (textEventArgs.Unicode == "\b" && Gamestate.NewPlayer.Nickname.Length != 0)
            {
                Gamestate.NewPlayer.Nickname = Gamestate.NewPlayer.Nickname.Remove(Gamestate.NewPlayer.Nickname.Length - 1);
            }
            else if (textEventArgs.Unicode != "\x1B" && textEventArgs.Unicode != "\t" && (Gamestate.NewPlayer.Nickname == null || Gamestate.NewPlayer.Nickname.Length < 10))
            {
                Gamestate.NewPlayer.Nickname += textEventArgs.Unicode;
            }

            input.ChangeText(Gamestate.NewPlayer.Nickname);
        }

        private void HandleColorChange(RangeInput input, int value)
        {
            Color currentColor = Gamestate.NewPlayer.SymbolData.color;
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

            Gamestate.NewPlayer.SymbolData.color = currentColor;
            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        private void ChangeButtonStates()
        {
            foreach (SelectSymbolButton button in SymbolButtons)
            {
                if (button.IconTexture == Gamestate.NewPlayer.SymbolData.texture)
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
            int id = Gamestate.SaveNewPlayer();
            Gamestate.ClearPlayersInGame();
            Gamestate.AddPlayerToGame(id);

            MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.Statistics });
        }
    }
}