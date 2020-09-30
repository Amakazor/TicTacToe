using SFML.Window;
using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

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
            MessageBus.Instance.PostEvent(MessageType.LoseFocus, sender, e);
            foreach(IRenderObject renderObject in RenderObjects)
            {
                if (renderObject.GetActor() is IClickable && renderObject.IsPointInside(e.X, e.Y))
                {
                    ((IClickable)renderObject.GetActor()).OnClick(e);
                }
            }
        }

        public void OnResize(object sender, SizeEventArgs e)
        {
            MessageBus.Instance.PostEvent(MessageType.ScreenResized, sender, e);
        }

        public void OnInput(object sender, TextEventArgs e)
        {
            MessageBus.Instance.PostEvent(MessageType.Input, sender, e);
        }
    }
}
