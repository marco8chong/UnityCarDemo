using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CarDemo
{
    public class ScoreCounting : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreText = null;

        [SerializeField]
        private int _targetScore = 30;

        [SerializeField]
        private bool _useTargetScore = true;

        public UnityEvent OnTargetReached;

        private int _score = 0;

        public int Score
        {
            get
            {
                return _score;
            }
        }

        private void OnEnable()
        {
            UpdateScore(0, true);
        }

        public void AddScore()
        {
            UpdateScore(1);
        }

        public void AddScore(int score)
        {
            UpdateScore(score);
        }

        public void ReduceScore()
        {
            UpdateScore(-1);
        }

        public void ReduceScore(int score)
        {
            UpdateScore(-score);
        }

        public void ResetScore()
        {
            _score = 0;
            UpdateScore();
        }

        public void UpdateScore(int deltaScore = 0, bool forceIgnoreTargetScore = false)
        {
            _score += deltaScore;

            if (_useTargetScore && (!forceIgnoreTargetScore))
            {
                if (_score == _targetScore)
                {
                    try
                    {
                        OnTargetReached?.Invoke();
                    }
                    catch(System.Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }

            if (_scoreText)
            {
                if (_useTargetScore)
                {
                    _scoreText.text = $"{ _score.ToString()} / {_targetScore}";
                }
                else
                {
                    _scoreText.text = _score.ToString();
                }
            }
        }
    }
}
