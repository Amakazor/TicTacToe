using System;
using System.Collections.Generic;

namespace TicTacToe.Utility
{
    public enum MessageType
    {
        Recalculate,
        Quit,
        FieldChanged,
        ChangeScreen,
        PreviousScreen,
        ScreenResized,
    }

    public class MessageBus
    {
        private static MessageBus instance;
        private Dictionary<MessageType, List<Action<object, EventArgs>>> listeners = new Dictionary<MessageType, List<Action<object, EventArgs>>>();

        private MessageBus() { }

        public static MessageBus Instance { get { return instance ?? (instance = new MessageBus()); } }

        public void Register(MessageType messageType, Action<object, EventArgs> listener)
        {
            if (!listeners.ContainsKey(messageType))
            {
                listeners[messageType] = new List<Action<object, EventArgs>>();
            }

            listeners[messageType].Add(listener);
        }

        public void Unregister(MessageType messageType, Action<object, EventArgs> listener)
        {
            if (listeners.ContainsKey(messageType) && listeners[messageType].Contains(listener))
            {
                listeners[messageType].Remove(listener);
            }
        }

        public void PostEvent(MessageType messageType, object sender, EventArgs eventArgs)
        {
            if (listeners.ContainsKey(messageType))
            {
                listeners[messageType].ForEach((Action<object, EventArgs> listener) => {
                    listener.Invoke(sender, eventArgs);
                });
            }
        }
    }
}
