using System;
using UnityEngine;

namespace GameItem
{
    /// <summary>
    /// Object defining the essence of an ingredient item
    /// </summary>
    public class IngredientItem : Ingredient
    {
        [SerializeField]
        private Rigidbody2D _rb;

        public event Action<IngredientItem> OnDestroy;
        public void Move(Vector2 direction) => _rb.velocity = direction;
        
        public void Destroyed()
        {
            OnDestroy?.Invoke(this);
            Destroy(gameObject);
        }
    }
}