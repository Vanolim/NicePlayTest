using System;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Input interface that allows using another service under this interface
    /// </summary>
    public interface IInputService
    {
        public Vector2 InputPosition { get; }
        public event Action OnInputDown;
        public event Action OnInputUp;
        public event Action OnRestart;
        public event Action OnDishCombination;
    }
}