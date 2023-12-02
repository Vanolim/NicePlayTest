using System;
using UnityEngine;

namespace GameItem
{
    /// <summary>
    /// Object determines what object has been placed inside it.
    /// It then emits an event about it and updates its view.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Cauldron : MonoBehaviour
    {
        [SerializeField]
        private CauldronIngredientView _cauldronIngredientView;
        
        [SerializeField]
        private CauldronAnimation _cauldronAnimation;
        
        public CauldronIngredientView CauldronIngredientView => _cauldronIngredientView;
        
        public event Action<IngredientItem> OnTakeIngredient;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IngredientItem ingredient))
            {
                _cauldronAnimation.ChangePreset(isWithIngredient: true);
                OnTakeIngredient?.Invoke(ingredient);
                ingredient.Destroyed();
            }
        }

        private void Start()
        {
            _cauldronAnimation.SetDefault();
        }

        public void Reset()
        {
            _cauldronAnimation.SetDefault();
            _cauldronIngredientView.Reset();
        }
    }
}