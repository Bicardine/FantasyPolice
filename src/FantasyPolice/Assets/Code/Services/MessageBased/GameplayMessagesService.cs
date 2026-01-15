using System;
using System.Collections.Generic;
using System.Linq;
using Services.MessageBased.Messages;

namespace Services.MessageBased
{
    public class GameplayMessagesService : IGameplayMessagesService
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Send<T>(T message) where T : IMessage
        {
            if (_subscribers.TryGetValue(typeof(T), out var handlers))
            {
                foreach (var handler in handlers.ToList())
                    ((Action<T>)handler)?.Invoke(message);
            }
        }

        public void Subscribe<T>(Action<T> handler) where T : IMessage
        {
            var type = typeof(T);
        
            if (_subscribers.ContainsKey(type) == false)
                _subscribers[type] = new List<Delegate>();
        
            _subscribers[type].Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler) where T : IMessage
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var handlers))
            {
                handlers.Remove(handler);
                if (handlers.Any() == false)
                {
                    _subscribers.Remove(type);
                }
            }
        }
    }
}