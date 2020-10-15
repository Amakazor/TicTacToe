using SFML.Graphics;
using SFML.Graphics.Glsl;
using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Actors;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;
using TicTacToe.Utility.Exceptions;

namespace TicTacToe.Game.GUI
{
    internal class Gui
    {
        public RenderWindow Window { get; private set; }
        public RenderBackground Background { get; private set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public Gamestate Gamestate { get; private set; }
        private TextureManager TextureManager;

        public float Time;
        public Frame Frame { get; private set; }

        public Gui(string gameTitle, uint width, uint height, Gamestate gamestate, TextureManager textureManager)
        {
            Width = width;
            Height = height;
            Gamestate = gamestate;
            TextureManager = textureManager;
            Time = 0;

            Window = new RenderWindow(new VideoMode(width, height), gameTitle, Styles.Default, new ContextSettings() { AntialiasingLevel = 8 });
            Window.SetVerticalSyncEnabled(true);
            Window.Closed += (_, __) => Window.Close();

            MessageBus.Instance.Register(MessageType.ScreenResized, ResizeWindow);

            AddBackground(width, height);

            Frame = new Frame(Gamestate);
        }

        public void Render(List<IRenderObject> RenderObjects, float deltaTime)
        {
            Window.DispatchEvents();
            Window.Clear(Color.White);

            Time = Time + deltaTime > 320 ? Time - 320 + deltaTime : Time + deltaTime;

            try
            {
                Shader shader = new Shader(null, null, "assets/shaders/scroll.glsl");
                shader.SetUniform("texture", Shader.CurrentTexture);
                shader.SetUniform("resolution", new Vec2(1024, 1024));
                shader.SetUniform("time", Time);
                Window.Draw((Drawable)Background.GetShape(), new RenderStates(shader));
            }
            catch (Exception)
            {
                throw new FileMissingOrCorruptedException("File scroll.glsl couln't be loaded");
            }

            Frame.RecalculateComponentsPositions();
            Frame.GetRenderObjects().ForEach(renderObject =>
            {
                Window.Draw((Drawable)renderObject.GetShape());
            });

            RenderObjects.ForEach((renderObject) =>
            {
                Window.Draw((Drawable)renderObject.GetShape());
            });

            Window.Display();
        }

        public void SetMouseClickHandler(EventHandler<MouseButtonEventArgs> mouseClickHandler)
        {
            Window.MouseButtonPressed += mouseClickHandler;
        }

        public void SetMouseReleaseHandler(EventHandler<MouseButtonEventArgs> mouseReleaseHandler)
        {
            Window.MouseButtonReleased += mouseReleaseHandler;
        }

        public void SetResizeHandler(EventHandler<SizeEventArgs> resizeHandler)
        {
            Window.Resized += resizeHandler;
        }

        public void SetInputHandlers(EventHandler<TextEventArgs> textHandler)
        {
            Window.TextEntered += textHandler;
        }

        private void ResizeWindow(object sender, EventArgs sizeEventArgs)
        {
            if (sizeEventArgs is SizeEventArgs)
            {
                Window.SetView(new View(new FloatRect(0.0F, 0.0F, ((SizeEventArgs)sizeEventArgs).Width, ((SizeEventArgs)sizeEventArgs).Height)));
                Width = ((SizeEventArgs)sizeEventArgs).Width;
                Height = ((SizeEventArgs)sizeEventArgs).Height;

                Background.SetSize((int)((SizeEventArgs)sizeEventArgs).Width, (int)((SizeEventArgs)sizeEventArgs).Height);
            }
            else throw new ArgumentException("Wrong EventArgs given", "sizeEventArgs");
        }

        private void AddBackground(uint width, uint height)
        {
            Background = new RenderBackground(new Position(0, 0, (int)width, (int)height), TextureManager.TexturesDictionary[TextureType.Background]["BG"]);
        }
    }
}