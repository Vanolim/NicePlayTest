using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    /// <summary>
    /// Object defining the sending restart event
    /// </summary>
    public class RestartButton : MonoBehaviour
    {
        [SerializeField]
        private Button _restart;

        public event Action OnRestart;

        private void SendRestartEvent() => OnRestart?.Invoke();

        private void OnEnable()
        {
            _restart.onClick.AddListener(SendRestartEvent);
        }
        
        private void OnDisable()
        {
            _restart.onClick.RemoveListener(SendRestartEvent);
        }
    }
}