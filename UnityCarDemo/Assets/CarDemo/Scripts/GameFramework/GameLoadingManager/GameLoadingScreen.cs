using TMPro;
using UnityEngine;

namespace CarDemo
{
    public class GameLoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _loadingText = null;

        public void Show()
        {
            _loadingText.text = "";
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetText(string text)
        {
            if (_loadingText)
            {
                _loadingText.text = text;
            }
        }
    }
}
