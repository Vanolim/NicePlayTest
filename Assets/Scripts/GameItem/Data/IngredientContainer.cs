using System.Linq;
using UnityEngine;

namespace GameItem
{
    /// <summary>
    /// Container of all ingredient items
    /// </summary>
    [CreateAssetMenu(fileName = "IngredientContainer", menuName = "GameData/IngredientContainer")]
    public class IngredientContainer : ScriptableObject
    {
        [SerializeField]
        private IngredientItem[] _ingredients;

        public IngredientItem GetIngredient(IngredientTypes ingredientType)
        {
            try
            {
                return _ingredients.FirstOrDefault(x => x.Type == ingredientType);
            }
            catch
            {
                Debug.LogError("IngredientContainer haven't " + ingredientType);
                throw;
            }
        }
    }
}