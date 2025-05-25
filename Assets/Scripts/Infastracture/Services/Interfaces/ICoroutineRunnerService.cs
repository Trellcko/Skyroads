using System.Collections;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure
{
    public interface ICoroutineRunnerService : IService
    {
        Coroutine StartCoroutine(IEnumerator name);
    }
}