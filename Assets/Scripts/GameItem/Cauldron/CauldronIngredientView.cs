using UnityEngine;

namespace GameItem
{
    /// <summary>
    /// Object for creating and modifying the display of ingredients inside the cauldron.
    /// </summary>
    public class CauldronIngredientView : MonoBehaviour
    {
        [SerializeField]
        private IngredientIcon ingredientIcon;

        [SerializeField]
        private RectTransform _contentContainer;

        private IngredientIcon[] _ingredientIcons;
        private int _countIngredients;

        //Method generates the required number of icons based on the maximum ingredient count
        public void Initialize(int maxCountIngredients)
        {
            _ingredientIcons = new IngredientIcon[maxCountIngredients];
            for (int i = 0; i < maxCountIngredients; i++)
            {
                IngredientIcon instance = Instantiate(ingredientIcon, _contentContainer);
                _ingredientIcons[i] = instance;
            }
        }

        //Change ingredient sprite
        public void SetIngredientSprite(Sprite sprite)
        {
            _ingredientIcons[_countIngredients].SetSprite(sprite);
            _countIngredients++;
        }

        //Reset view in icons
        public void Reset()
        {
            foreach (var ingredientIcon in _ingredientIcons)
            {
                ingredientIcon.ResetSprite();
            }

            _countIngredients = 0;
        }
    }
}