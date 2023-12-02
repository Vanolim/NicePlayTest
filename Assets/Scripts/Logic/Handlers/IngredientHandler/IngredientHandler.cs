using Core;
using GameItem;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object that processes ingredients
    /// </summary>
    public class IngredientHandler : IInitializable, ILateDisposable
    {
        private readonly IngredientItemMovement _ingredientItemMovement;
        private readonly IngredientItemSpawner _ingredientItemSpawner;
        private readonly InteractHandler _interactHandler;
        private readonly IInputService _inputService;
        private GameItem.Ingredient _target;

        public IngredientHandler(IngredientItemMovement ingredientItemMovement, IngredientItemSpawner ingredientItemSpawner, 
            InteractHandler interactHandler, IInputService inputService)
        {
            _ingredientItemMovement = ingredientItemMovement;
            _ingredientItemSpawner = ingredientItemSpawner;
            _interactHandler = interactHandler;
            _inputService = inputService;
        }

        private void SetTarget()
        {
            IInteractable target = _interactHandler.Target;

            if (target != default && target is GameItem.Ingredient ingredient)
            {
                _target = ingredient;
                _target.HideInteractive();
                _interactHandler.Deactivate();
                
                if (ingredient is IngredientItem ingredientItem)
                {
                    _ingredientItemMovement.StartMove(ingredientItem);
                    ingredientItem.OnDestroy += RemoveTarget;
                }
                else if (ingredient is IngredientSpot ingredientSpot)
                {
                    SpawnIngredientItem(ingredientSpot);
                }
            }
        }

        //Method that receives a new ingredient and initiates its movement
        private void SpawnIngredientItem(IngredientSpot ingredientSpot)
        {
            IngredientItem instantiateIngredientItem = _ingredientItemSpawner.Spawn(_inputService.InputPosition, ingredientSpot);
            _ingredientItemMovement.StartMove(instantiateIngredientItem);
            _target = instantiateIngredientItem;
            instantiateIngredientItem.OnDestroy += RemoveTarget;
        }

        private void RemoveTarget()
        {
            if (_target != default)
            {
                _ingredientItemMovement.StopMove();
                _target = default;
            }
            _interactHandler.Activate();
        }
        
        private void RemoveTarget(IngredientItem ingredient)
        {
            if (_target != default)
            {
                _ingredientItemMovement.StopMove();
                _target = default;
            }
            _interactHandler.Activate();
            ingredient.OnDestroy -= RemoveTarget;
        }
        
        public void Initialize()
        {
            _inputService.OnInputDown += SetTarget;
            _inputService.OnInputUp += RemoveTarget;
        }

        public void LateDispose()
        {
            _inputService.OnInputDown -= SetTarget;
            _inputService.OnInputUp -= RemoveTarget;
        }
    }
}