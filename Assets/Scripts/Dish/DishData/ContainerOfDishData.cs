using System.Collections.Generic;
using System.Linq;
using GameItem;
using UnityEngine;

namespace Dish
{
    /// <summary>
    /// Container of information about all dish and combo 
    /// </summary>
    [CreateAssetMenu(fileName = "DishDatas", menuName = "GameData/DishDatas")]
    public class ContainerOfDishData : ScriptableObject
    {
        [SerializeField]
        private DishData[] _dishDatas;

        [SerializeField]
        private ComboData[] _comboDatas;

        public IEnumerable<DishData> DishData => _dishDatas;

        public float GetComboData(int countElement)
        {
            ComboData comboData = _comboDatas.FirstOrDefault(x => x.CountIdenticalIngredients == countElement);
            if (comboData != null)
            {
                return comboData.Value;
            }

            return _comboDatas.Max(x => x.CountIdenticalIngredients);
        }
    }
}