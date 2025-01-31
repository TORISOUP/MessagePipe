﻿using System;
using Zenject;

namespace MessagePipe.Zenject
{
    internal struct DiContainerProxy : IServiceCollection
    {
        readonly DiContainer builder;

        public DiContainerProxy(DiContainer builder)
        {
            this.builder = builder;
        }

        public void AddTransient(Type type)
        {
            builder.Bind(type).AsTransient();
        }

        public void TryAddTransient(Type type)
        {
            if (!builder.HasBinding(type))
            {
                builder.Bind(type).AsTransient();
            }
        }

        public void AddSingleton<T>(T instance)
        {
            builder.BindInstance(instance).AsSingle();
        }

        public void AddSingleton(Type type)
        {
            builder.Bind(type).AsSingle();
        }

        public void Add(Type type, InstanceLifetime lifetime)
        {
            if (lifetime == InstanceLifetime.Scoped)
            {
                builder.Bind(type).AsCached();
            }
            else
            {
                builder.Bind(type).AsSingle();
            }
        }

        public void Add(Type serviceType, Type implementationType, InstanceLifetime lifetime)
        {
            if (lifetime == InstanceLifetime.Scoped)
            {
                builder.Bind(serviceType).To(implementationType).AsCached();
            }
            else
            {
                builder.Bind(serviceType).To(implementationType).AsSingle();
            }
        }
    }

    public sealed class ObjectResolverProxy : IServiceProvider
    {
        DiContainer resolver;

        public ObjectResolverProxy(DiContainer resolver)
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            return resolver.Resolve(serviceType);
        }
    }
}
