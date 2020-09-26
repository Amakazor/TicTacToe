using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
   abstract class Screen : IScreen
    {
        public Gamestate Gamestate { get; protected set; }
        public ScreenType EScreen { get; protected set; }
        public int ScreenWidth { get; protected set; }
        public int ScreenHeight { get; protected set; }

        public Screen(Gamestate gamestate, ScreenType eScreen)
        {
            Gamestate = gamestate;
            EScreen = eScreen;
        }

        public abstract List<IRenderObject> GetRenderData();

        public ScreenType GetEScreen()
        {
            return EScreen;
        }
    }
}
