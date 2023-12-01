using System.Collections;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Service for adding Coroutine logic to non-MonoBehaviour objects
    /// </summary>
    public class CoroutineService
    {
        //The object on which the Coroutine will be executed
        private readonly MonoBehaviour _target;

        public CoroutineService(MonoBehaviour target)
        {
            _target = target;
        }

        public void StartCoroutine(IEnumerator coroutine) =>
            _target.StartCoroutine(coroutine);
    }
}