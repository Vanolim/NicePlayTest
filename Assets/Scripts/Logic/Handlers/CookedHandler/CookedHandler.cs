using System;
using System.Collections.Generic;
using Core;
using GameItem;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object that assembles a cooked dish from ingredients
    /// </summary>
    public class CookedHandler : IInitializable, IRestartable
    {
        private readonly Cauldron _cauldron;
        private readonly IngredientItemsData _ingredientItemsData;
        private readonly RestartService _restartService;
        private readonly List<IngredientItemData> _ingredientsInCauldron = new();
        
        public event Action<IEnumerable<IngredientItemData>> OnCooked;

        public CookedHandler(Cauldron cauldron, IngredientItemsData ingredientItemsData, RestartService restartService)
        {
            _cauldron = cauldron;
            _ingredientItemsData = ingredientItemsData;
            _restartService = restartService;
        }

        private void AddIngredient(IngredientItem ingredientItem)
        {
            IngredientItemData ingredientItemData = _ingredientItemsData.GetData(ingredientItem.Type); 
            _ingredientsInCauldron.Add(ingredientItemData);
            _cauldron.CauldronIngredientView.SetIngredientSprite(ingredientItemData.IngredientIconSprite);
            Cooke();
        }

        //Reset cauldron and send event
        private void Cooke()
        {
            if (_ingredientsInCauldron.Count >= _ingredientItemsData.ValueIngredientsToCooked)
            {
                OnCooked?.Invoke(_ingredientsInCauldron);
                _ingredientsInCauldron.Clear();
                _cauldron.Reset();
            }
        }

        public void Initialize()
        {
            _cauldron.OnTakeIngredient += AddIngredient;
            _restartService.AddRestartable(this);
            _cauldron.CauldronIngredientView.Initialize(_ingredientItemsData.ValueIngredientsToCooked);
        }

        public void Restart()
        {
            _ingredientsInCauldron.Clear();
            _cauldron.Reset();
        }
    }
}