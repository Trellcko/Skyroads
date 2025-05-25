using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Trell.Skyroads.Infrastructure
{
    public static class ServiceLocator
    {
        public static event Action<Type> Registered;
 
        private static readonly Dictionary<Type, object> _services = new();
        
        public static bool IsRegistered<T>() => _services.ContainsKey(typeof(T));

        public static void Register<T>(T serviceInstance) where T : IService
        {
            Type key = typeof(T) == typeof(object) ? serviceInstance.GetType() : typeof(T);

            if (!_services.TryAdd(key, serviceInstance))
            {
                throw new Exception($"[Service Locator] Service \"{key}\" has already registered.");
            }

            Registered?.Invoke(key);
        }

        public static T Get<T>() where T : IService
        {
            Type key = typeof(T);
            if (!_services.ContainsKey(key))
            {
                throw new Exception($"[Service Locator] Service \"{key}\" not found.");
            }

            return (T)_services[key];
        }

        public static void Unregister<T>() where T : IService
        {
            Type key = typeof(T);
            if (!_services.ContainsKey(key))
            {
                throw new Exception($"[Service Locator] Cant unregister service \"{key}\" which is not registered");
            }

            _services.Remove(key);
        }
    }
}
