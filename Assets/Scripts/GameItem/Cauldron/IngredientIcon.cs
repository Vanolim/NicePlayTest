using UnityEngine;
using UnityEngine.UI;

namespace GameItem
{
    /// <summary>
    /// Object defining the essence of an ingredient icon
    /// </summary>
    public class IngredientIcon : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;
        
        [SerializeField]
        private Image _image;

        [SerializeField]
        private Sprite _default;

        [SerializeField]
        private float _iconScaleFactor;
        
        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
            Texture2D spriteTexture = _image.sprite.texture;
            _rectTransform.sizeDelta = new Vector2(spriteTexture.width * _iconScaleFactor, spriteTexture.height * _iconScaleFactor);
        }

        public void ResetSprite() => SetSprite(_default);
    }
}