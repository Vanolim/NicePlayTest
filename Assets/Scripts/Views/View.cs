using TMPro;
using UnityEngine;

namespace Views
{
    /// <summary>
    /// Abstraction defining a view to which a value can be assigned
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private string _addText;

        public void SetValue(string value) => _text.text = $"{_addText} {value}";
    }
}