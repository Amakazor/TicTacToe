using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Inputs
{
    class Input : TextBox, IClickable, IDisposable
    {
        public bool HasFocus { get; private set; }
        public int Id { get; private set; }
        private Action<Input, TextEventArgs> Action;

        public Input(Position position, Position relativeTextPosition, Gamestate gamestate, string text, Action<Input, TextEventArgs> action, int id) : base(position, relativeTextPosition, gamestate, text)
        {

            MessageBus.Instance.Register(MessageType.LoseFocus, OnLostFocus);
            MessageBus.Instance.Register(MessageType.Input, OnInput);
            Action = action;
            Id = id;
        }

        public void Dispose()
        {
            MessageBus.Instance.Unregister(MessageType.LoseFocus, OnLostFocus);
            MessageBus.Instance.Unregister(MessageType.Input, OnInput);
        }

        public void OnClick(MouseButtonEventArgs args)
        {
            MessageBus.Instance.PostEvent(MessageType.LoseFocus, this, args);
            HasFocus = true;
        }

        public void ChangeText(string text)
        {
            Text = text;
            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        private void OnLostFocus(object sender, EventArgs args)
        {
            if (sender != this)
            {
                HasFocus = false;
            }
        }

        protected void OnInput(object sender, EventArgs args)
        {
            if (HasFocus && args is TextEventArgs)
            {
                Action.Invoke(this, (TextEventArgs)args);
            }
        }
    }
}
