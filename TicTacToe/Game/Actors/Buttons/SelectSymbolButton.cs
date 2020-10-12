using SFML.Graphics;
using SFML.Window;
using System;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal class SelectSymbolButton : IconButton
    {
        public SelectSymbolButton(Position position, Gamestate gamestate, Texture symbolTexture, float scale, Color symbolColor) : base(position, gamestate, symbolTexture, scale, symbolColor) { }

        public override bool OnClick(MouseButtonEventArgs args)
        {
            if (base.OnClick(args))
            {
                Gamestate.NewPlayer.SymbolData.texture = IconTexture;
                MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
                return true;
            }

            return false;
        }
    }
}