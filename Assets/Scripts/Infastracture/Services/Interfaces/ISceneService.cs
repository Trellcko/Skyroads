using System;

namespace Trell.Skyroads.Infrastructure
{
    public interface ISceneService : IService
    {
        public void Load(string sceneName, Action onLoaded = null);
    }
}