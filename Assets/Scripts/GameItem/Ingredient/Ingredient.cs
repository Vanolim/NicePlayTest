using UnityEngine;

namespace GameItem
{
    /// <summary>
    /// Object defining the essence of an ingredient.
    /// Implements the IInteractable interface, allowing material changes
    /// </summary>
    public abstract class Ingredient : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private IngredientTypes _type;
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Material _defaultMaterial;
        
        [SerializeField]
        private Material _interactableMaterial;
        
        public IngredientTypes Type => _type;
        
        public void ShowInteractive() => _spriteRenderer.material = _interactableMaterial;
        public void HideInteractive() => _spriteRenderer.material = _defaultMaterial;
    }
}