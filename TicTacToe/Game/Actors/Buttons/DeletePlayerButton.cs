using SFML.Graphics;
using SFML.Window;
using System;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    class DeletePlayerButton : IconButton
    {
        public int PlayerId { get; }

        public DeletePlayerButton(Position position, Gamestate gamestate, Texture iconTexture, int playerId) : base(position, gamestate, iconTexture, 0.6F)
        {
            if (Gamestate.PlayersManager.Players.ContainsKey(playerId))
            {
                PlayerId = playerId;
            }
        }

        public override bool OnClick(MouseButtonEventArgs args)
        {
            if (base.OnClick(args))
            {
                Gamestate.PlayersManager.DeletePlayer(PlayerId);
                MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
                return true;
            }
            return false;
        }
    }
}
