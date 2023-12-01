using System.Collections.Generic;
using System.Linq;
using GameItem;

namespace Dish
{
    /// <summary>
    /// Object defining the prepared dish
    /// </summary>
    public class CookedDish
    {
        private readonly IEnumerable<IngredientItemData> _ingredients;
        private readonly string _name;
        private readonly int _worth;

        public int Worth => _worth;
        
        public CookedDish(IEnumerable<IngredientItemData> ingredients, string name, int worth)
        {
            _ingredients = ingredients;
            _name = name;
            _worth = worth;
        }
        
        //Method returns the names of ingredients and their quantities
        private Dictionary<string, int> GetIngredientsNameAndCount()
        {
            Dictionary<string, int> ingredientsNameAndCount = new Dictionary<string, int>();
            
            foreach (var ingredient in _ingredients)
            {
                if (ingredientsNameAndCount.Keys.Contains(ingredient.Name) == false)
                {
                    ingredientsNameAndCount.Add(ingredient.Name, _ingredients.Count(x => x.Type == ingredient.Type));
                }
            }

            return ingredientsNameAndCount;
        }
        
        //Method returns the object mapping data. It's constructed from the object types in the dish
        public string GetDishViewValue()
        {
            string ingredients = "(";
            var ingredientsNameAndCount = GetIngredientsNameAndCount();

            foreach (var ingredient in ingredientsNameAndCount)
            {
                ingredients += $"{ingredient.Key} {ingredient.Value}";
                if (ingredientsNameAndCount.Keys.Last() != ingredient.Key)
                {
                    ingredients += ", ";
                }
                else
                {
                    ingredients += ')';
                }
            }
            
            string value = $" {_name} {ingredients} [{_worth}]";
            return value;
        }
    }
}