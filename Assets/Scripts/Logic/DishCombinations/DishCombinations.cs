using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;
using Dish;
using GameItem;
using UnityEngine;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Object that finds all possible combinations of dishes and saves them to a file
    /// </summary>
    public class DishCombinations : IInitializable, ILateDisposable
    {
        private readonly IInputService _inputService;
        private readonly IngredientItemsData _ingredientItemsData;
        private readonly DishIdentifier _dishIdentifier;

        private const string _fileName = "AllDishCombination.txt";

        private string DestinationFile => Application.persistentDataPath + "/" + _fileName;

        public DishCombinations(IInputService inputService, IngredientItemsData ingredientItemsData, 
            DishIdentifier dishIdentifier)
        {
            _inputService = inputService;
            _ingredientItemsData = ingredientItemsData;
            _dishIdentifier = dishIdentifier;
        }
        
        private void SaveAllDishCombinationInFile()
        {
            string[] values = GetDishViewValues(GetAllCombinationIngredients(_ingredientItemsData.IngredientItemData, 
                _ingredientItemsData.ValueIngredientsToCooked));
            SaveToFile(values);
        }
        
        //Method that computes all possible combinations
        private IEnumerable<IEnumerable<IngredientItemData>> GetAllCombinationIngredients(List<IngredientItemData> ingredients, int countIngredientsInDish)
        {
            List<List<IngredientItemData>> combinations = new List<List<IngredientItemData>>();
            GenerateAllCombinations(ingredients, combinations, new List<IngredientItemData>(), index: 0, countIngredientsInDish);

            return combinations;
        }
        
        private void GenerateAllCombinations(List<IngredientItemData> ingredients, List<List<IngredientItemData>> result, List<IngredientItemData> current, int index, int size)
        {
            if (size == 0)
            {
                result.Add(new List<IngredientItemData>(current));
                return;
            }

            for (int i = index; i < ingredients.Count(); i++)
            {
                current.Add(ingredients[i]);
                GenerateAllCombinations(ingredients, result, current, i, size - 1);
                current.RemoveAt(current.Count - 1);
            }
        }

        //Method that provides information about all combinations and sorts them by worth
        private string[] GetDishViewValues(IEnumerable<IEnumerable<IngredientItemData>> ingredientCombinations)
        {
            List<CookedDish> allDishes = new List<CookedDish>();
            string[] allDishesViewValue = new string[ingredientCombinations.Count()];
            
            foreach (var combination in ingredientCombinations)
            {
                allDishes.Add(_dishIdentifier.GetDish(combination));
            }

            allDishes = allDishes.OrderBy(x => x.Worth).ToList();
            allDishesViewValue = new string[allDishes.Count()];
            
            for (int i = 0; i < allDishes.Count; i++)
            {
                allDishesViewValue[i] = allDishes[i].GetDishViewValue();
            }

            return allDishesViewValue;
        }

        //Method that saved data to file 
        private void SaveToFile(string[] value)
        {
            if (File.Exists(DestinationFile) == false)
            {
                File.WriteAllText(DestinationFile, "");
            }
            
            File.AppendAllLines(DestinationFile, value);
        }
        
        public void Initialize()
        {
            _inputService.OnDishCombination += SaveAllDishCombinationInFile;
        }

        public void LateDispose()
        {
            _inputService.OnDishCombination -= SaveAllDishCombinationInFile;
        }
    }
}