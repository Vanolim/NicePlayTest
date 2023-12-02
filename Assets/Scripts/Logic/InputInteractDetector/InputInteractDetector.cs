using System;
using System.Collections;
using Core;
using GameItem;
using UnityEngine;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object that detects when a user interacts with an interactive object
    /// </summary>
    public class InputInteractDetector : IInitializable
    {
        private readonly IInputService _inputService;
        private readonly CoroutineService _coroutineService;
        private Camera _camera;
        private IInteractable _target;

        private const float _addDetectedRadius = 0.5f;
        private const float _delayDetect = 0.1f;

        public event Action<IInteractable> OnStartDetect;
        public event Action OnFinishDetect;

        public InputInteractDetector(IInputService inputService, CoroutineService coroutineService)
        {
            _inputService = inputService;
            _coroutineService = coroutineService;
        }

        private IEnumerator DetectWork()
        {
            IInteractable target = default;
            while (true)
            {
                if ((TryDetectWithRay(ref target) || TryDetectWithCircleCast(ref target)))
                {
                    if (target != default && target != _target)
                    {
                        _target = target;
                        OnStartDetect?.Invoke(_target);
                    }
                }
                else
                {
                    if (_target != default)
                    {
                        _target = default;
                        OnFinishDetect?.Invoke();
                    }
                }

                yield return new WaitForSeconds(_delayDetect);
            }
        }

        //Method that attempts to detect IInteractable with a ray
        private bool TryDetectWithRay(ref IInteractable interactable)
        {
            RaycastHit2D hit = Physics2D.Raycast(_inputService.InputPosition, Vector2.zero);
            if (hit.collider != null &&
                hit.collider.TryGetComponent(out IInteractable targetInteractable))
            {
                interactable = targetInteractable;
                return true;
            }

            return false;
        }

        //Method that attempts to detect IInteractable with a circle
        private bool TryDetectWithCircleCast(ref IInteractable interactable)
        {
            RaycastHit2D hit = Physics2D.CircleCast(_inputService.InputPosition, _addDetectedRadius, Vector2.zero);
            if (hit.collider != null &&
                hit.collider.TryGetComponent(out IInteractable targetInteractable))
            {
                interactable = targetInteractable;
                return true;
            }

            return false;
        }

        public void Initialize()
        {
            _coroutineService.StartCoroutine(DetectWork());
        }
    }
}