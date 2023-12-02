using System.Collections.Generic;
using Core;
using GameItem;
using UnityEngine;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object that spawns an ingredient item from a spot and stores active ingredient items
    /// </summary>
    public class IngredientItemSpawner : IRestartable, IInitializable
    {
        private readonly IngredientContainer _ingredientContainer;
        private readonly RestartService _restartService;
        private readonly List<IngredientItem> _activeIngredients = new();
        
        public IngredientItemSpawner(IngredientContainer ingredientContainer, RestartService restartService)
        {
            _ingredientContainer = ingredientContainer;
            _restartService = restartService;
        }

        public IngredientItem Spawn(Vector2 spawnPosition, IngredientSpot ingredientSpot)
        {
            IngredientItem ingredientItem = _ingredientContainer.GetIngredient(ingredientSpot.Type);
            IngredientItem instance = GameObject.Instantiate(ingredientItem, spawnPosition, Quaternion.identity);
            
            _activeIngredients.Add(instance);
            instance.OnDestroy += RemoveIngredient;
            
            return instance;
        }

        private void RemoveIngredient(IngredientItem ingredientItem)
        {
            _activeIngredients.Remove(ingredientItem);
            ingredientItem.OnDestroy -= RemoveIngredient;
        }

        public void Restart()
        {
            for (int i = 0; i < _activeIngredients.Count; i++)
            {
                _activeIngredients[i].OnDestroy -= RemoveIngredient;
                _activeIngredients[i].Destroyed();
            }
            _activeIngredients.Clear();
        }

        public void Initialize()
        {
            _restartService.AddRestartable(this);
        }
    }
}