using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    class MessageButton : Button
    {
        public MessageType MessageType { get; }

        public MessageButton(Position position, Position relativeTextPosition, Gamestate gamestate, string text, MessageType messageType) : base(position, relativeTextPosition, gamestate, text)
        {
            MessageType = messageType;
        }

        public override void OnClick(MouseButtonEventArgs args)
        {
            MessageBus.Instance.PostEvent(MessageType, this, args);
        }
    }
}
