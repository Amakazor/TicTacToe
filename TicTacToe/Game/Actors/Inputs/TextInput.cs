using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Inputs
{
    class TextInput : Input, IDisposable, ITextContainer
    {
        protected string Text;
        protected Position RelativeTextPosition;
        public bool HasFocus { get; private set; }
        private Action<TextInput, TextEventArgs> Action;

        public TextInput(Position position, Position relativeTextPosition, Gamestate gamestate, string text, Action<TextInput, TextEventArgs> action, int id) : base(position, gamestate, id)
        {

            MessageBus.Instance.Register(MessageType.LoseFocus, OnLostFocus);
            MessageBus.Instance.Register(MessageType.Input, OnInput);
            Action = action;
            RelativeTextPosition = relativeTextPosition;
            Text = text;
        }

        public void Dispose()
        {
            MessageBus.Instance.Unregister(MessageType.LoseFocus, OnLostFocus);
            MessageBus.Instance.Unregister(MessageType.Input, OnInput);
        }

        public override void OnClick(MouseButtonEventArgs args)
        {
            MessageBus.Instance.PostEvent(MessageType.LoseFocus, this, args);
            HasFocus = true;
        }

        public void ChangeText(string text)
        {
            Text = text;
            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject>
            {
                new RenderRectangle(CalculateScreenSpacePosition(Position), this)
            };

            if (Text.Length > 0)
            {
                renderObjects.Add(new RenderText(CalculateScreenSpacePosition(((ITextContainer)this).CalculateTextPosition(Position, RelativeTextPosition)), this, Text));
            }

            return renderObjects;
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
