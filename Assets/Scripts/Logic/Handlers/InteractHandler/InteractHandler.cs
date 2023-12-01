using Core;
using GameItem;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object that interacts with interactive objects
    /// </summary>
    public class InteractHandler : IInitializable, ILateDisposable
    {
        private readonly IngredientMovement _ingredientMovement;
        private readonly IngredientSpawner _ingredientSpawner;
        private readonly InputInteractDetector _inputInteractDetector;
        private readonly IInputService _inputService;
        private IInteractable _target;

        public InteractHandler(IngredientMovement ingredientMovement, IngredientSpawner ingredientSpawner,
            InputInteractDetector inputInteractDetector, IInputService inputService)
        {
            _ingredientMovement = ingredientMovement;
            _ingredientSpawner = ingredientSpawner;
            _inputInteractDetector = inputInteractDetector;
            _inputService = inputService;
        }

        //Set the new interactable object
        private void SetTarget(IInteractable interactable)
        {
            if (_target != default)
            {
                _target.HideInteractive();
            }
            
            _target = interactable;
            _target.ShowInteractive();
        }

        //Remove the new interactable object
        private void RemoveTarget(IInteractable interactable)
        {
            if(_target == default)
                return;
            
            _target.HideInteractive();
            _target = default;
        }

        //Detect interact object (IngredientItem or IngredientSpot)
        private void DetectInteractObject()
        {
            if(_target == default)
                return;
            
            _target.HideInteractive();
            
            if (_target is IngredientItem ingredient)
            {
                _ingredientMovement.StartMove(ingredient);
                ingredient.OnDestroy += RemoveTarget;
            }
            else if (_target is IngredientSpot ingredientSpot)
            {
                SpawnIngredientItem(ingredientSpot);
            }
        }

        //Method that receives a new ingredient and initiates its movement
        private void SpawnIngredientItem(IngredientSpot ingredientSpot)
        {
            IngredientItem instatiateIngredientItem = _ingredientSpawner.Spawn(_inputService.InputPosition, ingredientSpot);
            _ingredientMovement.StartMove(instatiateIngredientItem);
            instatiateIngredientItem.OnDestroy += RemoveTarget;
        }

        private void FinishInteract()
        {
            if(_target == default)
                return;
            
            if (_target is IngredientItem)
            {
                _ingredientMovement.StopMove();
                _target.HideInteractive();
                _target = default;
            }
        }

        public void Initialize()
        {
            _inputInteractDetector.OnStartDetect += SetTarget;
            _inputInteractDetector.OnFinishDetect += RemoveTarget;
            _inputService.OnInputDown += DetectInteractObject;
            _inputService.OnInputUp += FinishInteract;
        }

        public void LateDispose()
        {
            _inputInteractDetector.OnStartDetect -= SetTarget;
            _inputInteractDetector.OnFinishDetect -= RemoveTarget;
            _inputService.OnInputDown -= DetectInteractObject;
            _inputService.OnInputUp -= FinishInteract;
        }
    }
}