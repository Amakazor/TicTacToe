using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    class Field : Actor, IRenderable, IEquatable<Field>, IClickable
    {
        public int PlayerID { get; private set; }
        public bool IsClickable { get; private set; }

        public Field (Position position, Gamestate gamestate) : base(position, gamestate)
        {
            PlayerID = 0;
            IsClickable = true;
        }

        public void SetValue (int newPlayer)
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

        public List<IRenderObject> GetRenderObjects()
        {
            List<IRenderObject> RenderObjects = new List<IRenderObject>{new RenderRectangle(Position, this, Color.White)};

            if (PlayerID != 0)
            {
                RenderObjects.AddRange(Gamestate.GetPlayersSymbolByPlayerID(PlayerID, Position).GetRenderObjects());
            }

            return RenderObjects;
        }
    }
}
