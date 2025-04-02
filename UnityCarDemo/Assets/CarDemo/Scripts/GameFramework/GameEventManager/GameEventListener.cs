using System;
using UnityEngine;
using UnityEngine.Events;

namespace CarDemo
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField]
        private GameEventSO _gameEventSO = null;

        [SerializeField]
        private bool _printDebugLog = true;

        public UnityEvent _onGameEvent = null;

        public void SetGameEvent(GameEventSO _listener)
        {
            _gameEventSO = _listener;
        }

        private void Start()
        {
            GameDirector.Instance.GameEventManager.OnGameEvent += GameEventManager_OnGameEvent;
        }

        private void OnDestroy()
        {
            GameDirector.Instance.GameEventManager.OnGameEvent -= GameEventManager_OnGameEvent;
        }

        private void GameEventManager_OnGameEvent(GameEventSO gameEvent, UnityEngine.Object sender)
        {
            if (gameEvent && (gameEvent == _gameEventSO))
            {
                if (_printDebugLog)
                {
                    Debug.Log($"[{this.name}] {gameEvent.GetDebugMessage()}");
                }

                try
                {
                    _onGameEvent?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}