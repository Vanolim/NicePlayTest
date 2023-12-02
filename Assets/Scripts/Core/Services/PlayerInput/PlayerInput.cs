using System;
using UnityEngine;
using Zenject;

namespace Core
{
    /// <summary>
    /// The script tracks player button presses
    /// </summary>
    public class PlayerInput : IInputService, IInitializable, ITickable
    {
        private Camera _camera;

        public Vector2 InputPosition => _camera.ScreenToWorldPoint(Input.mousePosition);

        public event Action OnInputDown;
        public event Action OnInputUp;
        public event Action OnRestart;
        public event Action OnDishCombination;

        public void Tick()
        {
            Debug.Log(Application.persistentDataPath);
            CheckInput();
        }

        private void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnInputDown?.Invoke();
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnInputUp?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                OnRestart?.Invoke();
            }
                
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDishCombination?.Invoke();
            }
        }

        public void Initialize()
        {
            _camera = Camera.main;
        }
    }
}