using System;
using UnityEngine;

namespace GameItem
{
    /// <summary>
    /// Object defining an ingredient. The default value is
    /// what will determine the worth of the ingredient
    /// if this data cannot be loaded from a file
    /// </summary>
    [Serializable]
    public class IngredientItemData
    {
        [field: SerializeField]
        public IngredientTypes Type { get; private set; }
        
        [field: SerializeField]
        public Sprite IngredientIconSprite { get; private set; }
        
        [field: SerializeField]
        public int DefaultValue { get; private set; }
        
        [field: SerializeField]
        public string Name { get; private set; }
    }
}