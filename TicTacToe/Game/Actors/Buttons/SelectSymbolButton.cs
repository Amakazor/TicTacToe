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
        public PlayersManager PlayersManager;

        public SelectSymbolButton(Position position, Gamestate gamestate, PlayersManager playersManager, Texture symbolTexture, float scale, Color symbolColor) : base(position, gamestate, symbolTexture, scale, symbolColor)
        {
            PlayersManager = playersManager;
        }

        public override bool OnClick(MouseButtonEventArgs args)
        {
            if (base.OnClick(args))
            {
                PlayersManager.NewPlayer.SymbolData.texture = IconTexture;
                MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
                return true;
            }

            return false;
        }
    }
}