using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SFML.Graphics;
using SFML.Window;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Actors.Inputs;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    class NewPlayerScreen : Screen
    {
        List<Actor> Actors;
        List<Symbol> Symbols;
        ActionButton CreatePlayerButton;
        public NewPlayerScreen(Gamestate gamestate) : base(gamestate, ScreenType.NewPlayer)
        {
            Gamestate.NewPlayer = new Player("", null, new Color(127, 127, 127), Gamestate);

            Actors = new List<Actor>();
            Actors.Add(new TextInput(new Position(25, 25, 950, 100), new Position(50, 30, 0, 30), Gamestate, "Enter players name (4 to 30 characters):", HandleNameChange, 0));
            Actors.Add(new RangeInput(new Position(50, 140, 900, 50), Gamestate, Gamestate.NewPlayer.SymbolData.color.R, byte.MinValue, byte.MaxValue, Color.Red, HandleColorChange, 1));
            Actors.Add(new RangeInput(new Position(50, 205, 900, 50), Gamestate, Gamestate.NewPlayer.SymbolData.color.G, byte.MinValue, byte.MaxValue, Color.Green, HandleColorChange, 2));
            Actors.Add(new RangeInput(new Position(50, 270, 900, 50), Gamestate, Gamestate.NewPlayer.SymbolData.color.B, byte.MinValue, byte.MaxValue, Color.Blue, HandleColorChange, 3));

            Symbols = new List<Symbol>();
            if (Gamestate.TextureAtlas.TexturesDictionary.ContainsKey(TextureType.Symbol))
            {
                int size = 200;
                int marginOuter = 25;
                int marginInner = ((1000 - 2 * marginOuter) - (size * 4)) / 3;

                int i = 0;
                foreach (KeyValuePair<string, Texture> rawSymbol in Gamestate.TextureAtlas.TexturesDictionary[TextureType.Symbol])
                {
                    Position curentPosition = new Position(marginOuter + i % 4 * (size + marginInner), 335 + (i / 4) * (size + marginInner), size, size);
                    Symbols.Add(new Symbol(new SymbolData { color = Gamestate.NewPlayer.SymbolData.color , texture = rawSymbol.Value}, curentPosition, Gamestate));
                    Actors.Add(new SelectSymbolButton(curentPosition, new Position(0, 0, 0, 0), Gamestate, "", Symbols[Symbols.Count-1]));
                    i++;
                }
            } else throw new Exception();

            CreatePlayerButton = new ActionButton(new Position(25, 845, 950, 130), new Position(50, 30, 0, 40), Gamestate, "Save Player", SaveNewPlayer);
        }

        public override void Dispose() {}

        public override List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();

            foreach (Actor actor in Actors)
            {
                renderObjects.AddRange(actor.GetRenderObjects());
            }

            foreach (Actor actor in Symbols)
            {
                renderObjects.AddRange(actor.GetRenderObjects());
            }

            if (Gamestate.ValidateNewPlayer())
            {
                renderObjects.AddRange(CreatePlayerButton.GetRenderObjects());
            }

            return renderObjects;
        }

        private void HandleNameChange(TextInput input, TextEventArgs textEventArgs)
        {
            if (textEventArgs.Unicode == "\b" && Gamestate.NewPlayer.Nickname.Length != 0)
            {
                Gamestate.NewPlayer.Nickname = Gamestate.NewPlayer.Nickname.Remove(Gamestate.NewPlayer.Nickname.Length - 1);
            }
            else if (textEventArgs.Unicode != "\x1B" && textEventArgs.Unicode != "\t" && (Gamestate.NewPlayer.Nickname == null || Gamestate.NewPlayer.Nickname.Length < 30))
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

            foreach (Symbol symbol in Symbols)
            {
                symbol.Color = currentColor;
            }

            Gamestate.NewPlayer.SymbolData.color = currentColor;
            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        private void SaveNewPlayer (MouseButtonEventArgs mouseButtonEventArgs)
        {
            int id = Gamestate.SaveNewPlayer();
            Gamestate.ClearPlayersInGame();
            Gamestate.AddPlayerToGame(id);

            MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenType.Players });
        }
    }
}
