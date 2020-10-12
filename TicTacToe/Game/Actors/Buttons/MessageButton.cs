using SFML.System;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal class MessageButton : TextButton
    {
        public MessageType MessageType { get; }

        public MessageButton(Position position, Gamestate gamestate, Vector2f margins, int fontSize, TextPosition horizontalPosition, TextPosition verticalPosition, string text, MessageType messageType) : base(position, gamestate, margins, fontSize, horizontalPosition, verticalPosition, text)
        {
            MessageType = messageType;
        }

        public override bool OnClick(MouseButtonEventArgs args)
        {
            if (base.OnClick(args))
            {
                MessageBus.Instance.PostEvent(MessageType, this, args);
                return true;
            }
            return false;
        }
    }
}