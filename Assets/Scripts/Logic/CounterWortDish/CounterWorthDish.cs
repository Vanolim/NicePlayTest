using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Dish;
using GameItem;
using UnityEngine;
using Views;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object defining the cost of a cooked dish
    /// </summary>
    public class CounterWorthDish : IInitializable, IRestartable
    {
        private readonly ContainerOfDishData _containerOfDishData;
        private readonly HUB _hub;
        private readonly RestartService _restartService;
        private readonly SaveLoadService _saveLoadService;
        private readonly IngredientItemWorth _ingredientItemWorth;
        private int _globalValue;

        public CounterWorthDish(ContainerOfDishData containerOfDishData, HUB hub, RestartService restartService, 
            SaveLoadService saveLoadService, IngredientItemWorth ingredientItemWorth)
        {
            _containerOfDishData = containerOfDishData;
            _hub = hub;
            _restartService = restartService;
            _saveLoadService = saveLoadService;
            _ingredientItemWorth = ingredientItemWorth;
        }

        //Method that outputs the value of a dish based on its ingredients
        public int GetWorthDish(IEnumerable<IngredientItemData> ingredients)
        {
            int value = default;
            List<IngredientTypes> countedIngredients = new List<IngredientTypes>();
            foreach (var ingredientData in ingredients)
            {
                if (countedIngredients.Contains(ingredientData.Type) == false)
                {
                    int countElement = ingredients.Count(x => x.Type == ingredientData.Type);
                    value += Convert.ToInt32(_ingredientItemWorth.GetValue(ingredientData.Name) 
                                             * countElement * _containerOfDishData.GetComboData(countElement));
                    countedIngredients.Add(ingredientData.Type);
                }
            }
            
            return value;
        }

        //Method that saves the best values of a dish
        public void AddGlobalWorthValue(int value)
        {
            _globalValue += value;
            _saveLoadService.SaveCounter(_globalValue);
            _hub.CounterView.SetValue(_globalValue.ToString());
        }

        public void Restart()
        {
            _globalValue = 0;
            _hub.CounterView.SetValue(_globalValue.ToString());
        }

        public void Initialize()
        {
            _restartService.AddRestartable(this);
            _globalValue = _saveLoadService.GetCounterValue();
            _hub.CounterView.SetValue(_globalValue.ToString());
        }
    }
}