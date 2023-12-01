using UnityEngine;

namespace GameItem
{
    /// <summary>
    /// Object responsible for the animation of the cauldron's coals smoldering
    /// </summary>
    public class CauldronAnimation : MonoBehaviour
    {
        [SerializeField]
        private CauldronAnimationPreset _withIngredient;
        
        [SerializeField]
        private CauldronAnimationPreset _withoutIngredient;

        [SerializeField]
        private SpriteRenderer _coals;

        [SerializeField]
        private float _presetChangeTime = 3;

        private CauldronAnimationPreset _currentPreset;
        private CauldronAnimationPreset _lastPreset;
        
        private bool _isWithIngredient;
        private float _currentTime;
        private bool _isChangingPreset;

        private void Update()
        {
            if (_isChangingPreset == false)
            {
                float alpha = _currentPreset.AnimationCurve.Evaluate(GetCurrentAnimationTime(_currentPreset.AnimationTime));
                SetCoalsAlpha(alpha);
            }
            else
            {
                ChangePreset();
            }
        }

        //Method smoothly changing the animation type
        private void ChangePreset()
        {
            if (_currentTime == 0)
            {
                _currentTime = 0;
                _isChangingPreset = false;
            }
                
            float alphaChangingPreset = Mathf.Lerp(_currentPreset.AnimationCurve.Evaluate(0), 0, GetCurrentAnimationTime(_presetChangeTime));
            SetCoalsAlpha(alphaChangingPreset);
        }
        
        private void SetCoalsAlpha(float alpha)
        {
            Color coalsHotColor = _coals.color;
            _coals.color = new Color(coalsHotColor.r, coalsHotColor.g, coalsHotColor.b, alpha);
        }

        public void SetDefault()
        {
            _isChangingPreset = false;
            _isWithIngredient = false;
            _currentPreset = _withoutIngredient;
        }

        public void ChangePreset(bool isWithIngredient)
        {
            if (_isWithIngredient != isWithIngredient)
            {
                _isChangingPreset = true;
                _isWithIngredient = isWithIngredient;
                if (_isWithIngredient)
                {
                    _currentPreset = _withIngredient;
                }
                else
                {
                    _currentPreset = _withoutIngredient;
                }
            }
        }

        //Method retrieve the time value based on the preset
        private float GetCurrentAnimationTime(float animationTime)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= animationTime)
            {
                _currentTime = 0;
            }
            
            return _currentTime / animationTime;
        }
    }
}