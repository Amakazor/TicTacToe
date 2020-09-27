using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Graphics.Glsl;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.GUI
{
    class Gui
    {
        public RenderWindow Window { get; private set; }
        public RenderBackground Background { get; private set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public Gamestate Gamestate { get; private set; }

        public float Time;

        public Gui(string gameTitle, uint width, uint height, Gamestate gamestate)
        {
            Width = width;
            Height = height;
            Gamestate = gamestate;
            Time = 0;

            Window = new RenderWindow(new VideoMode(width, height), gameTitle);
            Window.SetVerticalSyncEnabled(true);
            Window.Closed += (_, __) => Window.Close();

            MessageBus.Instance.Register(MessageType.ScreenResized, ResizeWindow);

            AddBackground(width, height);
        }

        public void Render(List<IRenderObject> RenderObjects, float deltaTime)
        {
            Window.DispatchEvents();
            Window.Clear(Color.White);
            Time = Time + deltaTime > 32 ? Time - 32 + deltaTime : Time + deltaTime;

            Shader shader = new Shader(null, null, "assets/shaders/scroll.glsl");
            shader.SetUniform("texture", Shader.CurrentTexture);
            shader.SetUniform("resolution", new Vec2(512, 512));
            shader.SetUniform("time", Time);

            Window.Draw((Drawable)Background.GetShape(), new RenderStates(shader));

            RenderObjects.ForEach((IRenderObject renderObject) => {
                Window.Draw((Drawable)renderObject.GetShape());
            });

            Window.Display();
        }

        public void SetInputHandlers(EventHandler<MouseButtonEventArgs> mouseClickHandler)
        {
            Window.MouseButtonPressed += mouseClickHandler;
        }

        public void SetResizeHandler(EventHandler<SizeEventArgs> resizeHandler)
        {
            Window.Resized += resizeHandler;
        }

        private void ResizeWindow(object sender, EventArgs sizeEventArgs)
        {
            if (sizeEventArgs is SizeEventArgs)
            {
                Window.SetView(new View(new FloatRect(0.0F, 0.0F, ((SizeEventArgs)sizeEventArgs).Width, ((SizeEventArgs)sizeEventArgs).Height)));
                Width = ((SizeEventArgs)sizeEventArgs).Width;
                Height = ((SizeEventArgs)sizeEventArgs).Height;

                uint smallerSize = ((SizeEventArgs)sizeEventArgs).Width > ((SizeEventArgs)sizeEventArgs).Height ? ((SizeEventArgs)sizeEventArgs).Height : ((SizeEventArgs)sizeEventArgs).Width;
                Background.SetSize(0, 0, (int)((SizeEventArgs)sizeEventArgs).Width, (int)((SizeEventArgs)sizeEventArgs).Height);
            }
            else throw new ArgumentException("Wrong EventArgs given", "sizeEventArgs");
        }

        private void AddBackground(uint width, uint height)
        {
            Background = new RenderBackground(0, 0, (int)width, (int)height, Gamestate.TextureAtlas.TexturesDictionary["BG"]);
        }
    }
}
