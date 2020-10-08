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

        private RenderRectangle FieldRectangle;

        public Field(Position position, Gamestate gamestate) : base(position, gamestate)
        {
            PlayerID = 0;
            IsClickable = true;

            FieldRectangle = new RenderRectangle(new Position(), this, new Color(), Color.Black, 2);
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

        public void OnClick(MouseButtonEventArgs args)
        {
            if (IsClickable)
            {
                SetValue(Gamestate.CurrentPlayer);
                IsClickable = false;
                MessageBus.Instance.PostEvent(MessageType.FieldChanged, this, new EventArgs());
            }
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            RecalculateComponentsPositions();
            List<IRenderObject> RenderObjects = new List<IRenderObject> { FieldRectangle };

            if (PlayerID != 0)
            {
                RenderObjects.AddRange(Gamestate.GetPlayersSymbolByPlayerID(PlayerID, Position).GetRenderObjects());
            }

            return RenderObjects;
        }

        public override void RecalculateComponentsPositions()
        {
            FieldRectangle.SetPosition(CalculateScreenSpacePosition(Position));
        }
    }
}