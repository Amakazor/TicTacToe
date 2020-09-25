using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using TicTacToe.Game.GUI.RenderObjects;

namespace TicTacToe.Game.GUI
{
    class Gui
    {
        public RenderWindow Window { get; private set; }

        public Gui(string gameTitle)
        {
            this.Window = new RenderWindow(new VideoMode(800, 600), gameTitle);
            this.Window.SetVerticalSyncEnabled(true);
            this.Window.Closed += (_, __) => this.Window.Close();
        }

        public void SetInputHandlers(EventHandler<MouseButtonEventArgs> mouseClickHandler)
        {
            this.Window.MouseButtonPressed += mouseClickHandler;
        }

        public void Render(List<IRenderObject> RenderObjects)
        {
            this.Window.DispatchEvents();
            this.Window.Clear(Color.White);


            RenderObjects.ForEach((IRenderObject renderObject) => {
                this.Window.Draw((Drawable)renderObject.GetShape());
            });

            this.Window.Display();
        }
    }
}
