using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    internal class Field : Actor, IRenderable, IEquatable<Field>, IClickable
    {
        public int PlayerID { get; private set; }
        public bool IsClickable { get; private set; }
        public PlayersManager PlayersManager;

        private RenderSymbol FieldBackgroundSymbol;


        public Field(Position position, Gamestate gamestate, PlayersManager playersManager, Texture texture) : base(position, gamestate)
        {
            PlayersManager = playersManager;

            PlayerID = 0;
            IsClickable = true;

            FieldBackgroundSymbol = new RenderSymbol(new Position(), this, texture, new Color(0, 0, 0, 255));
            RecalculateComponentsPositions();
        }

        public void SetValue(int newPlayer)
        {
            PlayerID = newPlayer;
        }

        public bool Equals(Field other)
        {
            return PlayerID == other.PlayerID;
        }

        public bool OnClick(MouseButtonEventArgs args)
        {
            if (IsClickable)
            {
                SetValue(PlayersManager.CurrentPlayer);
                IsClickable = false;
                MessageBus.Instance.PostEvent(MessageType.FieldChanged, this, new EventArgs());
                return true;
            }

            return false;
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            RecalculateComponentsPositions();
            List<IRenderObject> RenderObjects = new List<IRenderObject> { FieldBackgroundSymbol };

            if (PlayerID != 0)
            {
                RenderObjects.AddRange(PlayersManager.GetPlayersSymbolByPlayerID(PlayerID, Position).GetRenderObjects());
            }

            return RenderObjects;
        }

        public override void RecalculateComponentsPositions()
        {
            FieldBackgroundSymbol.SetPosition(CalculateScreenSpacePosition(Position));
        }
    }
}