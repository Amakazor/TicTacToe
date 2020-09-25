using SFML.Graphics;
using TicTacToe.Game.Actors;

namespace TicTacToe.Game.GUI.RenderObjects
{
    public interface IRenderObject
    {
        public IRenderable GetActor();

        public Transformable GetShape();

        public void SetSize(int positionX, int positionY, int width, int height);

        public bool IsPointInside(int x, int y);
    }
}
