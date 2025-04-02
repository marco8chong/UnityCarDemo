using NaughtyAttributes;
using UnityEngine;

namespace CarDemo
{
    public class GameEventTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameEventSO _gameEventToTrigger = null;

        [SerializeField]
        private bool _printDebugLog = true;

        [Button]
        public void TriggerGameEvent()
        {
            if (_gameEventToTrigger)
            {
                if (_printDebugLog)
                {
                    Debug.Log($"[GameEventTrigger] {this.name} - {_gameEventToTrigger.name}");
                }

                GameDirector.Instance.GameEventManager.CreateGameEvent(_gameEventToTrigger, this);            
            }
        }
    }
}
