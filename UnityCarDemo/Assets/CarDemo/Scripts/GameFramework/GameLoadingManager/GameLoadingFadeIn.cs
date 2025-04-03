using UnityEngine;
using UnityEngine.UI;

namespace CarDemo
{
    [RequireComponent(typeof(Image))]
    public class GameLoadingFadeIn : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve _fadingCurve = new AnimationCurve();

        [SerializeField]
        [Range(0.0f, 10.0f)]
        private float _fadingDuration = 5.0f;

        private Image _fadingImage = null;
        private float _timeElapsed = 0.0f;

        private void Awake()
        {
            _fadingImage = GetComponent<Image>();
            _fadingImage.color = new Color32(0, 0, 0, 255);
        }

        private void Update()
        {
            if (_fadingDuration > 0.0f)
            {
                _timeElapsed += Time.deltaTime;

                float fadingProgress = Mathf.Clamp01(_timeElapsed / _fadingDuration);
                _fadingImage.color = new Color32(0, 0, 0, (byte)(255.0f * _fadingCurve.Evaluate(fadingProgress)));

                if (fadingProgress >= 1.0f)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
