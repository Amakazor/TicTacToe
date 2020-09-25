using SFML.Window;
using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.GUI.RenderObjects;

namespace TicTacToe.Game.Events
{
    class InputHandler
    {
        private List<IRenderObject> RenderObjects { get; set; }

        public InputHandler(List<IRenderObject> renderObjects)
        {
            RenderObjects = renderObjects;
        }

        public void UpdateRenderObjects(List<IRenderObject> renderObjects)
        {
            RenderObjects = renderObjects;
        }

        public void OnClick(object sender, MouseButtonEventArgs e)
        {
            foreach(IRenderObject renderObject in RenderObjects)
            {
                if (renderObject.GetActor() is IClickable && renderObject.IsPointInside(e.X, e.Y))
                {
                    ((IClickable)renderObject.GetActor()).OnClick(e);
                }
            }
        }
    }
}
