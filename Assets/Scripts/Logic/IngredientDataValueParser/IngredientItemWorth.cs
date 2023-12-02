using System.Collections.Generic;
using System.Linq;
using GameItem;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object that provides the value of an ingredient
    /// </summary>
    public class IngredientItemWorth : IInitializable
    {
        private readonly IngredientItemsData _ingredientItemsData;
        private Dictionary<string, int> _ingredientsNameWorth;

        public IngredientItemWorth(IngredientItemsData ingredientItemsData)
        {
            _ingredientItemsData = ingredientItemsData;
        }

        //Method that attempts to retrieve the value of an ingredient from a file.
        //If unsuccessful, it uses the default value
        private void ParsIngredients()
        {
            IngredientItemXMLParser ingredientItemXMLParser = new IngredientItemXMLParser();
            _ingredientsNameWorth = ingredientItemXMLParser.Pars(_ingredientItemsData);
        }

        //Method that fills missing data with default values
        private void CheckParsIngredientData()
        {
            foreach (var ingredientItemData in _ingredientItemsData.IngredientItemData)
            {
                if (_ingredientsNameWorth.Keys.Contains(ingredientItemData.Name) == false)
                {
                    _ingredientsNameWorth.Add(ingredientItemData.Name, ingredientItemData.DefaultWorth);
                }
            }
        }

        public int GetValue(string ingredientName) => _ingredientsNameWorth[ingredientName];

        public void Initialize()
        {
            ParsIngredients();
        }
    }
}