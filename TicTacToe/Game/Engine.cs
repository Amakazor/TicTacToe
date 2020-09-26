using System;
using System.Collections.Generic;
using TicTacToe.Game.GUI;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Game.Events;
using TicTacToe.Utility;
using TicTacToe.Game.Data;
using TicTacToe.Game.Screens;

namespace TicTacToe.Game
{
    class Engine
    {
        public string GameTitle { get; private set; }
        public bool ShouldQuit { get; private set; }
        public bool ShouldRecalculateRenderObjects { get; set; }

        public Gamestate Gamestate { get; private set; }
        public Gui Gui { get; private set; }
        public InputHandler InputHandler { get; private set; }

        public List<IRenderObject> RenderObjects { get; private set; }

        public Engine()
        {
            uint width = 600;
            uint height = 600;

            GameTitle = "TicTacToe with SFML";
            ShouldQuit = false;
            ShouldRecalculateRenderObjects = true;

            Gamestate = new Gamestate();
            Gamestate.CurrentScreen = new MenuScreen(Gamestate);
            Gamestate.RecalculateScreenSize(width, height);

            RenderObjects = new List<IRenderObject>();
            InputHandler = new InputHandler(RenderObjects);

            Gui = new Gui(GameTitle, width, height);
            Gui.SetInputHandlers(InputHandler.OnClick);
            Gui.SetResizeHandler(InputHandler.OnResize);

            MessageBus.Instance.Register(MessageType.Recalculate, OnRecalculate);
            MessageBus.Instance.Register(MessageType.PreviousScreen, OnPreviousScreen);
            MessageBus.Instance.Register(MessageType.ChangeScreen, OnChangeScreen);
            MessageBus.Instance.Register(MessageType.Quit, OnQuit);
        }

        public void Loop()
        {
            while (Gui.Window.IsOpen && !ShouldQuit)
            {
                Tick();
            }
        }

        public void Tick()
        {
            if (ShouldRecalculateRenderObjects)
            {
                RenderObjects = Gamestate.CurrentScreen.GetRenderData();
                InputHandler.UpdateRenderObjects(RenderObjects);
                ShouldRecalculateRenderObjects = false;
            }

            Render(RenderObjects);
        }

        public void Render(List<IRenderObject> RenderObjects)
        {
            Gui.Render(RenderObjects);
        }

        public void OnRecalculate(object sender, EventArgs eventArgs)
        {
            ShouldRecalculateRenderObjects = true;
        }

        public void OnChangeScreen(object sender, EventArgs eventArgs)
        {
            if (eventArgs is ChangeScreenEventArgs)
            {
                Gamestate.PreviousScreen = Gamestate.CurrentScreen.GetEScreen();
                Gamestate.CurrentScreen.Dispose();

                switch (((ChangeScreenEventArgs)eventArgs).Screen)
                {
                    case ScreenType.Game:
                        Gamestate.SetCurrentPlayerToFirstEntry();
                        Gamestate.CurrentScreen = new GameScreen(Gamestate);
                        break;
                    case ScreenType.Pregame:
                        Gamestate.CurrentScreen = new PregameScreen(Gamestate);
                        break;
                    case ScreenType.PlayerSelectionScreen:
                        Gamestate.CurrentScreen = new PlayerSelectionScreen(Gamestate);
                        break;
                    case ScreenType.MenuScreen:
                        Gamestate.CurrentScreen = new MenuScreen(Gamestate);
                        break;
                    case ScreenType.Results:
                        Gamestate.CurrentScreen = new ResultsScreen(Gamestate);
                        break;
                    default:
                        throw new Exception();
                }

                ShouldRecalculateRenderObjects = true;
            }
        }

        public void OnPreviousScreen(object sender, EventArgs eventArgs)
        {
            OnChangeScreen(sender, new ChangeScreenEventArgs { Screen = Gamestate.PreviousScreen });
        }

        public void OnQuit(object sender, EventArgs eventArgs)
        {
            ShouldQuit = true;
        }
    }
}
