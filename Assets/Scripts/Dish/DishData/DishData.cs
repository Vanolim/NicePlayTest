using System;
using GameItem;
using UnityEngine;

namespace Dish
{
    /// <summary>
    /// Information about the dish and the types of ingredients it contains.
    /// The 'PresenceType' indicates whether the required ingredient is present or absent in the dish
    /// </summary>
    [Serializable]
    public class DishData
    {
        [field: SerializeField]
        public IngredientTypes Type { get; private set; }

        [field: SerializeField] 
        public DishDataIngredientPresenceType DishDataIngredientPresenceType { get; private set; }

        [field: SerializeField]
        public int MinCountElements { get; private set; }

        [field: SerializeField]
        public int MaxCountElements { get; private set; }

        [field: SerializeField]
        public int Priority { get; private set; }

        [field: SerializeField] 
        public string Name { get; private set; }
    }
}