using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameItem
{
    /// <summary>
    /// Container of all ingredient items and maximum quantity values of ingredients for cooking a dish
    /// </summary>
    [CreateAssetMenu(fileName = "IngredientItemsData", menuName = "GameData/IngredientItemsData")]
    public class IngredientItemsData : ScriptableObject
    {
        [SerializeField]
        private IngredientItemData[] _ingredientItemsData;

        [SerializeField]
        private int _valueIngredientsToCooked = 5;

        public int ValueIngredientsToCooked => _valueIngredientsToCooked;
        public List<IngredientItemData> IngredientItemData => _ingredientItemsData.ToList();

        public IngredientItemData GetData(IngredientTypes ingredientType)
        {
            try
            {
                return _ingredientItemsData.FirstOrDefault(x => x.Type == ingredientType);
            }
            catch
            {
                Debug.LogError("Ingredients value haven't " + ingredientType);
                throw;
            }
        }
    }
}