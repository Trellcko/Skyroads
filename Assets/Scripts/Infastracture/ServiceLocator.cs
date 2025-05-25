using System;
using System.Collections.Generic;

namespace Trell.Skyroads.Infrastructure
{
    public class ServiceLocator
    {
        
        public static ServiceLocator Instance => _instance ??= new();
        
        private static ServiceLocator _instance;
        
        public static event Action<Type> Registered;
 
        private static readonly Dictionary<Type, object> _services = new();
        
        public static bool IsRegistered<T>() => _services.ContainsKey(typeof(T));

        public void Register<T>(T serviceInstance) where T : IService
        {
            Type key = typeof(T) == typeof(object) ? serviceInstance.GetType() : typeof(T);

            if (!_services.TryAdd(key, serviceInstance))
            {
                throw new Exception($"[Service Locator] Service \"{key}\" has already registered.");
            }

            Registered?.Invoke(key);
        }

        public T Get<T>() where T : IService
        {
            Type key = typeof(T);
            if (!_services.TryGetValue(key, out object service))
            {
                throw new Exception($"[Service Locator] Service \"{key}\" not found.");
            }

            return (T)service;
        }

        public void Unregister<T>() where T : IService
        {
            Type key = typeof(T);
            if (!_services.Remove(key))
            {
                throw new Exception($"[Service Locator] Cant unregister service \"{key}\" which is not registered");
            }
        }
    }
}
