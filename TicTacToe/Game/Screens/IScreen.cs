using System.Collections.Generic;
using TicTacToe.Game.GUI.RenderObjects;

namespace TicTacToe.Game.Screens
{
    public interface IScreen
    {
        public List<IRenderObject> GetRenderData();

        public ScreenType GetEScreen();
    }
}