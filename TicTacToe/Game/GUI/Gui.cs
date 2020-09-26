using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI
{
    class Gui
    {
        public RenderWindow Window { get; private set; }

        public Gui(string gameTitle, uint width, uint height)
        {
            Window = new RenderWindow(new VideoMode(width, height), gameTitle);
            Window.SetVerticalSyncEnabled(true);
            Window.Closed += (_, __) => Window.Close();

            MessageBus.Instance.Register(MessageType.ScreenResized, ResizeWindow);
        }

        public void SetInputHandlers(EventHandler<MouseButtonEventArgs> mouseClickHandler)
        {
            Window.MouseButtonPressed += mouseClickHandler;
        }

        public void SetResizeHandler(EventHandler<SizeEventArgs> resizeHandler)
        {
            Window.Resized += resizeHandler;
        }

        public void ResizeWindow(object sender, EventArgs sizeEventArgs)
        {
            if (sizeEventArgs is SizeEventArgs)
            {
                Window.SetView(new View(new FloatRect(0.0F, 0.0F, ((SizeEventArgs)sizeEventArgs).Width, ((SizeEventArgs)sizeEventArgs).Height)));
            }
            else throw new ArgumentException("Wrong EventArgs given", "sizeEventArgs");
        }

        public void Render(List<IRenderObject> RenderObjects)
        {
            Window.DispatchEvents();
            Window.Clear(Color.White);

            RenderObjects.ForEach((IRenderObject renderObject) => {
                Window.Draw((Drawable)renderObject.GetShape());
            });

            Window.Display();
        }
    }
}
