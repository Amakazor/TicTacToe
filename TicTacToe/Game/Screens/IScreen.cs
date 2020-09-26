using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Screens
{
    public interface IScreen
    {
        public List<IRenderObject> GetRenderData();
        public ScreenType GetEScreen();
    }
}
