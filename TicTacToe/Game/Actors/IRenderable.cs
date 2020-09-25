using System.Collections.Generic;
using TicTacToe.Game.GUI.RenderObjects;

namespace TicTacToe.Game.Actors
{
    public interface IRenderable
    {
        public List<IRenderObject> GetRenderObjects();
    }
}
