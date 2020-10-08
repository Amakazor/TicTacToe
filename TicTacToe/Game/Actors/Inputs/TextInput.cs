using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Inputs
{
    internal class TextInput : Input, IDisposable, ITextContainer
    {
        protected string Text;
        public bool HasFocus { get; private set; }
        private Action<TextInput, TextEventArgs> Action;
        public int FontSize { get; }

        private RenderRectangle InputRectangle;
        private AlignedRenderText InputText;

        public TextInput(Position position, Gamestate gamestate, Vector2f margins, int fontSize, TextPosition horizontalPosition, TextPosition verticalPosition, string text, Action<TextInput, TextEventArgs> action, int id) : base(position, gamestate, id)
        {
            MessageBus.Instance.Register(MessageType.LoseFocus, OnLostFocus);
            MessageBus.Instance.Register(MessageType.Input, OnInput);
            Action = action;
            FontSize = fontSize;
            Text = text;

            InputRectangle = new RenderRectangle(new Position(), this);
            InputText = new AlignedRenderText(new Position(), margins, 0, horizontalPosition, verticalPosition, this, Color.Black, Text);

            RecalculateComponentsPositions();
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
            InputText.Text = Text;
            MessageBus.Instance.PostEvent(MessageType.Recalculate, this, new EventArgs());
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            List<IRenderObject> renderObjects = new List<IRenderObject> { InputRectangle };

            if (Text.Length > 0)
            {
                renderObjects.Add(InputText);
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

        public override void RecalculateComponentsPositions()
        {
            InputRectangle.SetPosition(CalculateScreenSpacePosition(Position));
            InputText.SetPosition(CalculateScreenSpacePosition(Position));
            InputText.SetFontSize(CalculateScreenSpaceHeight(FontSize));
        }
    }
}