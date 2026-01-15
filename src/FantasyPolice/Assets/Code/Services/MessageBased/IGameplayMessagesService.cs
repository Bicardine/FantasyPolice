using System;
using Services.MessageBased.Messages;

namespace Services.MessageBased
{
    public interface IGameplayMessagesService
    {
        void Send<T>(T message) where T : IMessage;
        void Subscribe<T>(Action<T> handler) where T : IMessage;
        void Unsubscribe<T>(Action<T> handler) where T : IMessage;
    }
}