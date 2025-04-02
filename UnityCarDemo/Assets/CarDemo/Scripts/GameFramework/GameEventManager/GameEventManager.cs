using UnityEngine;

namespace CarDemo
{
    public class GameEventManager : GameDirectorService
    {
        public delegate void GameEventAction(GameEventSO gameEvent, UnityEngine.Object sender);

        public event GameEventAction OnGameEvent;

        [SerializeField]
        private bool _printEvent = true;

        public void CreateGameEvent(GameEventSO gameEvent, UnityEngine.Object sender)
        {
            if (_printEvent)
            {
                string gameEventName = gameEvent ? gameEvent.name : "null";
                string senderName = sender ? sender.name : "null";
                Debug.Log($"[GameEvent {gameEventName}] Sender = {senderName}, Debug Message = {gameEvent.GetDebugMessage()}");
            }

            OnGameEvent?.Invoke(gameEvent, sender);
        }
    }
}
