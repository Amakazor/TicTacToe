using SFML.Graphics;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    public interface IRenderObject
    {
        public IRenderable GetActor();

        public Transformable GetShape();

        public void SetSize(int width, int height);

        public void SetPosition(Position position);

        public bool IsPointInside(int x, int y);
    }
}
