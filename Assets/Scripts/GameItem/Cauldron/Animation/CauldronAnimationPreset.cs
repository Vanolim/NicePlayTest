using UnityEngine;

namespace GameItem
{
    /// <summary>
    /// Preset time and animation for the burning of the cauldron's coals.
    /// </summary>
    [CreateAssetMenu(fileName = "CauldronAnimationPreset", menuName = "GameData/CauldronAnimationPreset")]
    public class CauldronAnimationPreset : ScriptableObject
    {
        [field: SerializeField]
        public float AnimationTime { get; private set; }
        
        [field: SerializeField]
        public AnimationCurve AnimationCurve { get; private set; }
    }
}