using System;
using System.Collections.Generic;

namespace Assets.MainGame.Team.BR.Code.Classes.MessageBus
{
    public static class MessageBus
    {
        // Dictionary to store subscribers for each message type
        private static Dictionary<Type, List<Action<object>>> subscribers = new Dictionary<Type, List<Action<object>>>();

        // Method to subscribe to a message type
        public static void Subscribe<T>(Action<object> callback)
        {
            Type messageType = typeof(T);

            // If the message type is not already in the dictionary, add it
            if (!subscribers.ContainsKey(messageType))
            {
                subscribers[messageType] = new List<Action<object>>();
            }

            // Add the callback to the list of subscribers for the message type
            subscribers[messageType].Add(callback);
        }

        public static void UnSubscribe<T>(Action<object> callback)
        {
            Type messageType = typeof(T);

            if (subscribers.ContainsKey(messageType))
            {
                if (subscribers[messageType].Contains(callback))
                {
                    subscribers[messageType].Remove(callback);
                }
            }
        }

        // Method to publish a message

        public static void Publish<T>(T message)
        {
            Type messageType = typeof(T);

            // Check if there are subscribers for the message type
            if (subscribers.ContainsKey(messageType))
            {
                // Invoke each callback subscribed to the message type and pass the message
                foreach (var subscriber in subscribers[messageType])
                {
                    subscriber.Invoke(message);
                }
            }
        }
    }
}