using System;
using System.Collections.Generic;
using SFML.Window;
using TicTacToe.Game.Actors.Buttons;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    class MenuScreen : Screen, IScreen
    {
        private List<Button> Buttons;
        public MenuScreen(Gamestate gamestate) : base(gamestate, EScreens.MenuScreen)
        {
            Buttons = new List<Button>();
            Buttons.Add(new ScreenChangeButton(new Position(200, 0, 600, 100), new Position(100, 25, 0, 40), Gamestate, "New Game", EScreens.Pregame));
            Buttons.Add(new MessageButton(new Position(200, 120, 600, 100), new Position(100, 25, 0, 40), Gamestate, "Quit", MessageType.Quit));
        }

        public override List<IRenderObject> GetRenderData()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>();
            foreach (Button button in Buttons)
            {
                renderObjects.AddRange(button.GetRenderObjects());
            }

            return renderObjects;
        }
    }
}
