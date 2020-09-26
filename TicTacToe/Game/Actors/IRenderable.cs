using System.Collections.Generic;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    public interface IRenderable
    {
        List<IRenderObject> GetRenderObjects();
        Position CalculatePosition(Position basePosition);
    }
}
