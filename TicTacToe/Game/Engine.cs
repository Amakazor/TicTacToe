﻿using System;
using System.Collections.Generic;
using TicTacToe.Game.GUI;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Game.Events;
using TicTacToe.Utility;
using TicTacToe.Game.Data;
using TicTacToe.Game.Screens;
using System.Linq;

namespace TicTacToe.Game
{
    class Engine
    {
        public string GameTitle { get; private set; }
        public bool ShouldQuit { get; private set; }
        public bool ShouldRecalculateRenderObjects { get; set; }

        public Gamestate Gamestate { get; private set; }
        //public Game Game { get; private set; }
        //public Screens CurrentScreen { get; private set; }
        public Gui Gui { get; private set; }
        public InputHandler InputHandler { get; private set; }

        public List<IRenderObject> RenderObjects { get; private set; }

        public Engine()
        {
            GameTitle = "TicTacToe with SFML";
            ShouldQuit = false;
            ShouldRecalculateRenderObjects = true;

            Gamestate = new Gamestate();
            //Gamestate.BoardSize = 3;
            //Gamestate.SetPlayersInGame(new List<int> { 1, 2 });
            //Gamestate.SetCurrentPlayer(1);

            //Gamestate.CurrentScreen = new GameScreen(Gamestate);
            //Gamestate.CurrentScreen = new PregameScreen(Gamestate);
            //Gamestate.CurrentScreen = new PlayerSelectionScreen(Gamestate);

            Gamestate.CurrentScreen = new PregameScreen(Gamestate);

            RenderObjects = new List<IRenderObject>();
            InputHandler = new InputHandler(RenderObjects);

            Gui = new Gui(GameTitle);
            Gui.SetInputHandlers(InputHandler.OnClick);

            MessageBus.Instance.Register(MessageType.Recalculate, OnRecalculate);
            MessageBus.Instance.Register(MessageType.PreviousScreen, OnPreviousScreen);
            MessageBus.Instance.Register(MessageType.ChangeScreen, OnChangeScreen);
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
                ShouldRecalculateRenderObjects = true;

                switch (((ChangeScreenEventArgs)eventArgs).Screen)
                {
                    case EScreens.Game:
                        Gamestate.SetCurrentPlayerToFirstEntry();
                        Gamestate.CurrentScreen = new GameScreen(Gamestate);
                        break;
                    case EScreens.Pregame:
                        Gamestate.CurrentScreen = new PregameScreen(Gamestate);
                        break;
                    case EScreens.PlayerSelectionScreen:
                        Gamestate.CurrentScreen = new PlayerSelectionScreen(Gamestate);
                        break;
                    default:
                        throw new Exception();
                }
            }
        }

        public void OnPreviousScreen(object sender, EventArgs eventArgs)
        {
            OnChangeScreen(sender, new ChangeScreenEventArgs { Screen = Gamestate.PreviousScreen });
        }
    }
}