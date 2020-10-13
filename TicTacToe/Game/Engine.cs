using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Game.Screens;

namespace TicTacToe.Game
{
    internal class Engine
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
            Gamestate.RecalculateScreenSize(width, height);

            Gamestate.CurrentScreen = new MenuScreen(Gamestate);

            RenderObjects = new List<IRenderObject>();

            Gui = new Gui(GameTitle, width, height, Gamestate);

            InputHandler = new InputHandler(RenderObjects, Gui.Window);
            Gui.SetMouseClickHandler(InputHandler.OnClick);
            Gui.SetMouseReleaseHandler(InputHandler.OnRelease);
            Gui.SetResizeHandler(InputHandler.OnResize);
            Gui.SetInputHandlers(InputHandler.OnInput);

            MessageBus.Instance.Register(MessageType.Recalculate, OnRecalculate);
            MessageBus.Instance.Register(MessageType.PreviousScreen, OnPreviousScreen);
            MessageBus.Instance.Register(MessageType.ChangeScreen, OnChangeScreen);
            MessageBus.Instance.Register(MessageType.Quit, OnQuit);
        }

        public void Loop()
        {
            DateTime time1 = DateTime.Now;
            DateTime time2 = DateTime.Now;

            while (Gui.Window.IsOpen && !ShouldQuit)
            {
                time2 = DateTime.Now;
                float deltaTime = (time2.Ticks - time1.Ticks) / 10000000f;
                Tick(deltaTime);
                time1 = time2;
            }
        }

        public void Tick(float deltaTime)
        {
            InputHandler.Tick();

            if (ShouldRecalculateRenderObjects)
            {
                RenderObjects = Gamestate.CurrentScreen.GetRenderData();
                InputHandler.UpdateRenderObjects(RenderObjects);
                ShouldRecalculateRenderObjects = false;
            }

            Render(RenderObjects, deltaTime);
        }

        public void Render(List<IRenderObject> RenderObjects, float deltaTime)
        {
            Gui.Render(RenderObjects, deltaTime);
        }

        public void OnRecalculate(object sender, EventArgs eventArgs)
        {
            ShouldRecalculateRenderObjects = true;
        }

        public void OnChangeScreen(object sender, EventArgs eventArgs)
        {
            if (eventArgs is ChangeScreenEventArgs)
            {
                if (Gamestate.CurrentScreen.GetEScreen() != ((ChangeScreenEventArgs)eventArgs).Screen && ((ChangeScreenEventArgs)eventArgs).ChangePreviousScreen)
                {
                    Gamestate.SecondPreviousScreen = Gamestate.PreviousScreen;
                    Gamestate.PreviousScreen = Gamestate.CurrentScreen.GetEScreen();
                }

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

                    case ScreenType.Statistics:
                        Gamestate.CurrentScreen = new StatisticsScreen(Gamestate);
                        break;

                    case ScreenType.NewPlayer:
                        Gamestate.CurrentScreen = new NewPlayerScreen(Gamestate);
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