using System.Collections.Generic;
using Dish;
using GameItem;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object that links a dish with its associated views and set new value counterWorthDish
    /// </summary>
    public class DishHandler : IInitializable, ILateDisposable
    {
        private readonly CookedHandler _cookedHandler;
        private readonly DishIdentifier _dishIdentifier;
        private readonly CounterWorthDish _counterWorthDish;
        private readonly BestDish _bestDish;
        private readonly LastDish _lastDish;

        public DishHandler(CookedHandler cookedHandler, DishIdentifier dishIdentifier, 
            BestDish bestDish, LastDish lastDish, CounterWorthDish counterWorthDish)
        {
            _cookedHandler = cookedHandler;
            _dishIdentifier = dishIdentifier;
            _bestDish = bestDish;
            _lastDish = lastDish;
            _counterWorthDish = counterWorthDish;
        }

        private void SetNewDish(IEnumerable<IngredientItemData> ingredients)
        {
            var cookedDish = _dishIdentifier.GetDish(ingredients);
            _counterWorthDish.AddGlobalWorthValue(cookedDish.Worth);
            _bestDish.SetDish(cookedDish);
            _lastDish.SetDish(cookedDish);
        }

        public void Initialize()
        {
            _cookedHandler.OnCooked += SetNewDish;
        }

        public void LateDispose()
        {
            _cookedHandler.OnCooked -= SetNewDish;
        }
    }
}