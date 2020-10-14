using SFML.Graphics;
using SFML.Window;
using System;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal class DeletePlayerButton : IconButton
    {
        public int PlayerId { get; }
        public PlayersManager PlayersManager { get; }

        public DeletePlayerButton(Position position, Gamestate gamestate, PlayersManager playersManager, Texture iconTexture, int playerId) : base(position, gamestate, iconTexture, 0.6F)
        {
            PlayersManager = playersManager;

            if (PlayersManager.Players.ContainsKey(playerId))
            {
                PlayerId = playerId;
            }
        }

        public override bool OnClick(MouseButtonEventArgs args)
        {
            if (base.OnClick(args))
            {
                PlayersManager.DeletePlayer(PlayerId);
                MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
                return true;
            }
            return false;
        }
    }
}