using SFML.Graphics;
using SFML.System;
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

        private bool IsMouseButtonHeld = false;
        private RenderWindow Window { get; set; }

        private MouseButtonEventArgs MouseButtonEventArgs;
        private Actor HeldActor { get; set; }

        public InputHandler(List<IRenderObject> renderObjects, RenderWindow window)
        {
            RenderObjects = renderObjects;
            Window = window;
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

                    if (renderObject.GetActor() is IHoldable)
                    {
                        HeldActor = (Actor)(renderObject.GetActor());
                    }
                }
            }

            MouseButtonEventArgs = e;
            IsMouseButtonHeld = true;
        }
        
        public void OnRelease(object sender, MouseButtonEventArgs e)
        {
            IsMouseButtonHeld = false;
            MouseButtonEventArgs = null;
        }

        public void OnResize(object sender, SizeEventArgs e)
        {
            MessageBus.Instance.PostEvent(MessageType.ScreenResized, sender, e);
        }

        public void OnInput(object sender, TextEventArgs e)
        {
            MessageBus.Instance.PostEvent(MessageType.Input, sender, e);
        }

        public void Tick()
        {
            if (IsMouseButtonHeld && MouseButtonEventArgs != null && HeldActor != null)
            {
                Vector2i mousePosition = Mouse.GetPosition(Window);

                List<IRenderObject> renderObjects = HeldActor.GetRenderObjects();

                foreach (IRenderObject renderObject in renderObjects)
                {
                    if (renderObject.GetActor() is IHoldable && renderObject.IsPointInside(mousePosition.X, mousePosition.Y))
                    {
                        ((IHoldable)renderObject.GetActor()).OnClick(new MouseButtonEventArgs(new MouseButtonEvent() { Button = MouseButtonEventArgs.Button, X = mousePosition.X, Y = mousePosition.Y }));
                    }
                }
            }
        }
    }
}
