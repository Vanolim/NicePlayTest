using System;
using UnityEngine;

namespace GameItem
{
    /// <summary>
    /// Object defining the value of a combo and the required quantity of ingredients for it
    /// </summary>
    [Serializable]
    public class ComboData
    {
        [field: SerializeField]
        public int CountIdenticalIngredients { get; private set; }
        
        [field: SerializeField]
        public float Value { get; private set; }
    }
}