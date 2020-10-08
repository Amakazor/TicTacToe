using SFML.Graphics;
using TicTacToe.Game.Actors;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI.RenderObjects
{
    internal class RenderBackground : IRenderObject
    {
        public Position Position;
        public Texture Texture { get; set; }

        public RenderBackground(Position position, Texture texture)
        {
            Position = position;
            Texture = texture;
        }

        public IRenderable GetActor()
        {
            return null;
        }

        public Transformable GetShape()
        {
            Texture.Repeated = true;

            Sprite sprite = new Sprite(Texture);
            sprite.TextureRect = new IntRect(0, 0, Position.Width, Position.Height);

            return sprite;
        }

        public bool IsPointInside(int x, int y)
        {
            return false;
        }

        public void SetSize(int width, int height)
        {
            Position.Width = width;
            Position.Height = height;
        }

        public void SetPosition(Position position)
        {
            Position = position;
        }
    }
}