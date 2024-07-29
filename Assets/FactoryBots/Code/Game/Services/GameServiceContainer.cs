using System;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryBots.Game.Services
{
    public class GameServiceContainer
    {
        private readonly Dictionary<Type, IGameService> _services = new Dictionary<Type, IGameService>();

        public void RegisterSingle<TService>(TService implementation) where TService : class, IGameService
        {
            Type serviceType = typeof(TService);

            if (_services.ContainsKey(serviceType))
            {
                Debug.LogError($"Service of type {serviceType} is already registered.");
            }

            _services[serviceType] = implementation;
        }

        public TService Single<TService>() where TService : class, IGameService
        {
            Type serviceType = typeof(TService);

            if (_services.TryGetValue(serviceType, out IGameService service))
            {
                return (TService)service;
            }

            Debug.LogError($"Service of type {serviceType} is not registered.");
            return null;
        }

        public void Cleanup()
        {
            foreach (IGameService service in _services.Values)
            {
                service.Cleanup();
            }

            _services.Clear();
        }
    }
}
