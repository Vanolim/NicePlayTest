using System.Collections.Generic;
using System.Linq;
using Dish;
using GameItem;

namespace Logic
{
    /// <summary>
    /// Object that defines a dish from ingredients
    /// </summary>
    public class DishIdentifier
    {
        private readonly ContainerOfDishData _containerOfDishData;
        private readonly CounterWorthDish _counterWorthDish;
        
        public DishIdentifier(ContainerOfDishData containerOfDishData, CounterWorthDish counterWorthDish)
        {
            _containerOfDishData = containerOfDishData;
            _counterWorthDish = counterWorthDish;
        }

        public CookedDish GetDish(IEnumerable<IngredientItemData> ingredients)
        {
            string name = GetDishName(ingredients);
            int worth = _counterWorthDish.GetWorthDish(ingredients);
            Dish.CookedDish cookedDish = new Dish.CookedDish(ingredients, name, worth);
            return cookedDish;
        }

        //Get dish name from dish data
        private string GetDishName(IEnumerable<IngredientItemData> ingredients)
        {
            List<DishData> tryDishDatas = new List<DishData>();
            foreach (var ingredient in ingredients)
            {
                int countIngredientOfType = ingredients.Count(x => x.Type == ingredient.Type);
                if (TryGetDishDataOfIngredientType(ingredient.Type, countIngredientOfType, out DishData dishData))
                {
                    if (tryDishDatas.Contains(dishData) == false)
                    {
                        tryDishDatas.Add(dishData);
                    }
                }
            }
            
            tryDishDatas.AddRange(GetAbsenceDishDatas(ingredients));
            return tryDishDatas.OrderBy(x => x.Priority).Last().Name;
        }

        private bool TryGetDishDataOfIngredientType(IngredientTypes ingredientType,
            int countIngredients, out DishData dishDataOfIngredientType)
        {
            List<DishData> dishDatasOfIngredientType = new List<DishData>();
            foreach (var dishData in _containerOfDishData.DishData)
            {
                if (dishData.DishDataIngredientPresenceType == DishDataIngredientPresenceType.Presence 
                    && (dishData.Type == ingredientType || dishData.Type == IngredientTypes.None))
                {
                    dishDatasOfIngredientType.Add(dishData);
                }
            }
            
            List<DishData> unsuitableDishDatas = new List<DishData>();
            for (int i = 0; i < dishDatasOfIngredientType.Count; i++)
            {
                DishData dishData = dishDatasOfIngredientType[i];
                if (DishDataOfIngredientTypeIsValidate(dishData, countIngredients) == false)
                {
                    unsuitableDishDatas.Add(dishData);
                }
            }

            var suitableDishDatas = dishDatasOfIngredientType.Except(unsuitableDishDatas);
            
            if (suitableDishDatas.Any() == false)
            {
                dishDataOfIngredientType = default;
                return false;
            }
            
            dishDataOfIngredientType = suitableDishDatas.OrderBy(x => x.Priority).Last();
            return true;
        }

        private List<DishData> GetAbsenceDishDatas(IEnumerable<IngredientItemData> ingredients)
        {
            List<DishData> absenceDishDatas = new List<DishData>();
            foreach (var dishData in _containerOfDishData.DishData)
            {
                if (dishData.DishDataIngredientPresenceType ==
                    DishDataIngredientPresenceType.Absence
                    && ingredients.FirstOrDefault(x => x.Type == dishData.Type) == null)
                {
                    absenceDishDatas.Add(dishData);
                }
            }

            return absenceDishDatas;
        }

        private bool DishDataOfIngredientTypeIsValidate(DishData dishData, int countIngredients)
        {
            if (countIngredients < dishData.MinCountElements)
                return false;

            if (dishData.MaxCountElements == 0)
                return true;

            return countIngredients <= dishData.MaxCountElements;
        }
    }
}