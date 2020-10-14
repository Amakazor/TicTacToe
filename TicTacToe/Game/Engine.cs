using SFML.Window;
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

        private TextureManager TextureManager;
        private PlayersManager PlayersManager;
        private StatisticsManager StatisticsManager;

        public Gamestate Gamestate { get; private set; }
        public Gui Gui { get; private set; }
        public InputHandler InputHandler { get; private set; }

        public List<IRenderObject> RenderObjects { get; private set; }

        public ScreenSize ScreenSize { get; private set; }

        public Engine()
        {
            uint width = 600;
            uint height = 600;

            Gamestate = new Gamestate();

            RecalculateScreenSize(width, height);

            GameTitle = "TicTacToe with SFML";
            ShouldQuit = false;
            ShouldRecalculateRenderObjects = true;

            TextureManager = new TextureManager();
            PlayersManager = new PlayersManager(TextureManager, Gamestate);
            StatisticsManager = new StatisticsManager();

            Gamestate.PlayersManager = PlayersManager;

            Gamestate.CurrentScreen = new MenuScreen(Gamestate, PlayersManager);

            Gui = new Gui(GameTitle, width, height, Gamestate, TextureManager);

            InputHandler = new InputHandler(Gui.Window);
            Gui.SetMouseClickHandler(InputHandler.OnClick);
            Gui.SetMouseReleaseHandler(InputHandler.OnRelease);
            Gui.SetResizeHandler(InputHandler.OnResize);
            Gui.SetInputHandlers(InputHandler.OnInput);

            MessageBus.Instance.Register(MessageType.Recalculate, OnRecalculate);
            MessageBus.Instance.Register(MessageType.PreviousScreen, OnPreviousScreen);
            MessageBus.Instance.Register(MessageType.ChangeScreen, OnChangeScreen);
            MessageBus.Instance.Register(MessageType.Quit, OnQuit);
            MessageBus.Instance.Register(MessageType.ScreenResized, OnResize);
        }

        public void Loop()
        {
            DateTime time1 = DateTime.Now;
            DateTime time2;

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
                        PlayersManager.SetCurrentPlayerToFirstEntry();
                        Gamestate.CurrentScreen = new GameScreen(Gamestate, PlayersManager);
                        break;

                    case ScreenType.Pregame:
                        Gamestate.CurrentScreen = new PregameScreen(Gamestate, PlayersManager, TextureManager);
                        break;

                    case ScreenType.PlayerSelectionScreen:
                        Gamestate.CurrentScreen = new PlayerSelectionScreen(Gamestate, PlayersManager, TextureManager, StatisticsManager);
                        break;

                    case ScreenType.MenuScreen:
                        Gamestate.CurrentScreen = new MenuScreen(Gamestate, PlayersManager);
                        break;

                    case ScreenType.Results:
                        Gamestate.CurrentScreen = new ResultsScreen(Gamestate, PlayersManager, TextureManager, StatisticsManager);
                        break;

                    case ScreenType.Statistics:
                        Gamestate.CurrentScreen = new StatisticsScreen(Gamestate, PlayersManager, TextureManager, StatisticsManager);
                        break;

                    case ScreenType.NewPlayer:
                        Gamestate.CurrentScreen = new NewPlayerScreen(Gamestate, PlayersManager, TextureManager);
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

        public void OnResize(object sender, EventArgs sizeEventArgs)
        {
            if (sizeEventArgs is SizeEventArgs)
            {
                RecalculateScreenSize(((SizeEventArgs)sizeEventArgs).Width, ((SizeEventArgs)sizeEventArgs).Height);
            }
            else throw new ArgumentException("Wrong EventArgs given", "sizeEventArgs");
        }

        public void RecalculateScreenSize(uint width, uint height)
        {
            const double marginPercentage = 0.1D;
            uint smallerSize = width > height ? height : width;

            uint newWidth = (uint)Math.Floor(smallerSize * (1.0D - 2.0D * marginPercentage));
            uint newHeight = newWidth;
            uint newMarginTop = (uint)Math.Floor((height - newHeight) / 2.0D);
            uint newMarginLeft = (uint)Math.Floor((width - newWidth) / 2.0D);
            uint newTotalHeight = newHeight + newMarginTop * 2;
            uint newTotalWidth = newWidth + newMarginLeft * 2;

            ScreenSize = new ScreenSize(newWidth, newHeight, newMarginTop, newMarginLeft, newTotalWidth, newTotalHeight);
            Gamestate.ScreenSize = ScreenSize;

            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }
    }
}