using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace MessagePipe
{
    // handler

    public interface IMessageHandler<TMessage>
    {
        void Handle(TMessage message);
    }

    public interface IAsyncMessageHandler<TMessage>
    {
        UniTask HandleAsync(TMessage message, CancellationToken cancellationToken);
    }

    // Keyless

    public interface IPublisher<TMessage>
    {
        void Publish(TMessage message);
    }

    public interface ISubscriber<TMessage>
    {
        IDisposable Subscribe(IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters);
    }

    public interface IAsyncPublisher<TMessage>
    {
        void Publish(TMessage message, CancellationToken cancellationToken = default(CancellationToken));
        UniTask PublishAsync(TMessage message, CancellationToken cancellationToken = default(CancellationToken));
        UniTask PublishAsync(TMessage message, AsyncPublishStrategy publishStrategy, CancellationToken cancellationToken = default(CancellationToken));
    }

    public interface IAsyncSubscriber<TMessage>
    {
        IDisposable Subscribe(IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters);
    }

    // Keyed

    public interface IPublisher<TKey, TMessage>
        
    {
        void Publish(TKey key, TMessage message);
    }

    public interface ISubscriber<TKey, TMessage>
        
    {
        IDisposable Subscribe(TKey key, IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters);
    }

    public interface IAsyncPublisher<TKey, TMessage>
        
    {
        void Publish(TKey key, TMessage message, CancellationToken cancellationToken = default(CancellationToken));
        UniTask PublishAsync(TKey key, TMessage message, CancellationToken cancellationToken = default(CancellationToken));
        UniTask PublishAsync(TKey key, TMessage message, AsyncPublishStrategy publishStrategy, CancellationToken cancellationToken = default(CancellationToken));
    }

    public interface IAsyncSubscriber<TKey, TMessage>
        
    {
        IDisposable Subscribe(TKey key, IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters);
    }
}