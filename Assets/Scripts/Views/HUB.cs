using UnityEngine;

namespace Views
{
    /// <summary>
    /// Container of all views
    /// </summary>
    public class HUB : MonoBehaviour
    {
        [field: SerializeField] 
        public CounterView CounterView { get; private set; }
        
        [field: SerializeField]
        public LastDishView LastDishView { get; private set; }
        
        [field: SerializeField]
        public BestDishView BestDishView { get; private set; }
        
        [field: SerializeField]
        public RestartButton RestartButton { get; private set; }
    }
}