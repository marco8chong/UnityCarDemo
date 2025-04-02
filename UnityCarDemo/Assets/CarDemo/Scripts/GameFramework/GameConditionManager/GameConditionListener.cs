using UnityEngine;
using UnityEngine.Events;

namespace CarDemo
{
    public class GameConditionListener : MonoBehaviour
    {
        [SerializeField]
        private GameConditionSO _targetGameCondition = null;

        [SerializeField]
        private bool _printLog = true;

        public UnityEvent OnGameConditionRegistered = null;
        public UnityEvent OnGameConditionUnregistered = null;

        private void Start()
        {
            GameDirector.Instance.GameConditionManager.OnConditionRegistered += GameConditionManager_OnConditionRegistered;
            GameDirector.Instance.GameConditionManager.OnConditionUnregistered += GameConditionManager_OnConditionUnregistered;
        }

        private void OnDestroy()
        {
            GameDirector.Instance.GameConditionManager.OnConditionRegistered -= GameConditionManager_OnConditionRegistered;
            GameDirector.Instance.GameConditionManager.OnConditionUnregistered -= GameConditionManager_OnConditionUnregistered;
        }

        private void GameConditionManager_OnConditionRegistered(GameConditionSO gameConditionSO)
        {
            if (gameConditionSO == _targetGameCondition)
            {
                if (_printLog)
                {
                    Debug.Log($"[GameConditionListener] Registered - {gameConditionSO.name}");
                }

                OnGameConditionRegistered?.Invoke();
            }
        }

        private void GameConditionManager_OnConditionUnregistered(GameConditionSO gameConditionSO)
        {
            if (gameConditionSO == _targetGameCondition)
            {
                if (_printLog)
                {
                    Debug.Log($"[GameConditionListener] Unregistered - {gameConditionSO.name}");
                }

                OnGameConditionUnregistered?.Invoke();
            }
        }

        public bool CheckCondition()
        {
            return GameDirector.Instance.GameConditionManager.CheckGameCondition(_targetGameCondition);
        }
    }
}
