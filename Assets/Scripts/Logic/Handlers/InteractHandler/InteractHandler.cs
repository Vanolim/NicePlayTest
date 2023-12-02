using Core;
using GameItem;
using Zenject;

namespace Logic
{
    /// <summary>
    /// object that processes interactive objects
    /// </summary>
    public class InteractHandler : IInitializable, ILateDisposable, IActivable
    {
        private readonly InputInteractDetector _inputInteractDetector;
        private IInteractable _target;
        private bool _isActive;

        public IInteractable Target => _target;

        public InteractHandler(InputInteractDetector inputInteractDetector)
        {
            _inputInteractDetector = inputInteractDetector;
        }

        //Set the new interactable object
        private void SetTarget(IInteractable interactable)
        {
            if (_isActive)
            {
                if (_target != default)
                {
                    _target.HideInteractive();
                }
            
                _target = interactable;
                _target.ShowInteractive();
            }
        }

        //Remove the new interactable object
        private void RemoveTarget()
        {
            if(_target == default)
                return;
            
            _target.HideInteractive();
            _target = default;
        }
        
        public void Initialize()
        {
            _isActive = true;
            _inputInteractDetector.OnStartDetect += SetTarget;
            _inputInteractDetector.OnFinishDetect += RemoveTarget;
        }

        public void LateDispose()
        {
            _inputInteractDetector.OnStartDetect -= SetTarget;
            _inputInteractDetector.OnFinishDetect -= RemoveTarget;
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
            _target = default;
        }
    }
}