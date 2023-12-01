using System.Collections.Generic;
using System.Linq;
using Core;
using GameItem;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object that provides the value of an ingredient
    /// </summary>
    public class IngredientDataValue : IInitializable
    {
        private IngredientItemsData _ingredientItemsData;
        private List<Ingredient> _ingredients;
        private readonly FileService _fileService;

        public IngredientDataValue(FileService fileService)
        {
            _fileService = fileService;
        }

        //Method that attempts to retrieve the value of an ingredient from a file.
        //If unsuccessful, it uses the default value
        private void ParsIngredients()
        {
            IngredientXMLParser ingredientXMLParser = new IngredientXMLParser();
            try
            {
                if (ingredientXMLParser.TryPars(_fileService, out _ingredients) == false)
                {
                    SetDefaultValue();
                }
            }
            catch
            {
                SetDefaultValue();
            }
        }

        private void SetDefaultValue()
        {
            _ingredients = new List<Ingredient>();
            foreach (var ingredientData in _ingredientItemsData.IngredientItemData)
            {
                Ingredient ingredient = new Ingredient();
                ingredient.Name = ingredientData.Name;
                ingredient.Value = ingredientData.DefaultValue;
                _ingredients.Add(ingredient);
            }
        }

        public int GetValue(string ingredientName) => 
            _ingredients.First(x => x.Name == ingredientName).Value;

        public void Initialize()
        {
            ParsIngredients();
        }
    }
}