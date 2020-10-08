using SFML.System;
using System.Collections.Generic;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    internal class MenuScreen : Screen, IScreen
    {
        private List<TextButton> Buttons;

        public MenuScreen(Gamestate gamestate) : base(gamestate, ScreenType.MenuScreen)
        {
            Gamestate.Clear();

            Buttons = new List<TextButton>();
            Buttons.Add(new ScreenChangeButton(new Position(200, 0, 600, 100), Gamestate, new Vector2f(), 40, TextPosition.Middle, TextPosition.Middle, "New Game", ScreenType.Pregame));
            Buttons.Add(new ScreenChangeButton(new Position(200, 120, 600, 100), Gamestate, new Vector2f(), 40, TextPosition.Middle, TextPosition.Middle, "Players", ScreenType.Players));
            Buttons.Add(new MessageButton(new Position(200, 240, 600, 100), Gamestate, new Vector2f(), 40, TextPosition.Middle, TextPosition.Middle, "Quit", MessageType.Quit));
        }

        public override void Dispose() { }

        public override List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();

            foreach (TextButton button in Buttons)
            {
                button.RecalculateComponentsPositions();
                renderObjects.AddRange(button.GetRenderObjects());
            }

            return renderObjects;
        }
    }
}