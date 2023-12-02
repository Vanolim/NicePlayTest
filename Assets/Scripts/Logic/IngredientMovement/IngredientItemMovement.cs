using Core;
using UnityEngine;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object responsible for object movement
    /// </summary>
    public class IngredientItemMovement : IFixedTickable
    {
        private readonly IInputService _inputService;
        private GameItem.IngredientItem _target;

        private const float _moveSpeed = 20f;

        public IngredientItemMovement(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Move()
        {
            Vector2 direction = _inputService.InputPosition - (Vector2)_target.transform.position;
            Vector2 clampMovementDirection =
                Vector3.ClampMagnitude(direction / Time.deltaTime, _moveSpeed);
            _target.Move(clampMovementDirection);
        }

        public void FixedTick()
        {
            if(_target != default)
                Move();
        }

        public void StartMove(GameItem.IngredientItem target) => _target = target;

        public void StopMove() => _target = default;
    }
}