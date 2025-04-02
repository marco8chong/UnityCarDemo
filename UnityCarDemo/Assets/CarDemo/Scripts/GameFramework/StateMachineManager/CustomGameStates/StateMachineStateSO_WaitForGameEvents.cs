using System;
using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "StateMachineStateSO_WaitForGameEvent", menuName = "Car Demo/SO/Game State/Wait for Game Events")]
    public class StateMachineStateSO_WaitForGameEvents : StateMachineStateSO
    {
        [Serializable]
        private class TargetGameEvent
        {
            public GameEventSO GameEvent;
            public bool Received;
        }

        [SerializeField]
        private List<GameEventSO> _targetGameEvents = new List<GameEventSO>();

        private List<TargetGameEvent> _waitingGameEvents = new List<TargetGameEvent>();

        public override void StateStart(StateMachine stateMachine)
        {
            base.StateStart(stateMachine);

            GameDirector.Instance.GameEventManager.OnGameEvent += GameEventManager_OnGameEvent;
            Init();
        }

        public override void StateTick()
        {
            base.StateTick();

            if (IsReceivedAllEvents())
            {
                EndCurrentState();
            }
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }

        private void GameEventManager_OnGameEvent(GameEventSO gameEvent, UnityEngine.Object sender)
        {
            foreach (TargetGameEvent targetGameEvent in _waitingGameEvents)
            {
                if (targetGameEvent != null)
                {
                    if (targetGameEvent.GameEvent == gameEvent)
                    {
                        targetGameEvent.Received = true;
                    }
                }
            }
        }

        private void Init()
        {
            _waitingGameEvents.Clear();

            foreach (GameEventSO targetGameEvent in _targetGameEvents)
            {
                if (targetGameEvent != null)
                {
                    TargetGameEvent newEvent = new TargetGameEvent();
                    newEvent.GameEvent = targetGameEvent;
                    newEvent.Received = false;
                    
                    _waitingGameEvents.Add(newEvent);
                }
            }
        }

        private bool IsReceivedAllEvents()
        {
            bool receivedAll = true;

            int i = 0;
            while (receivedAll && (i < _waitingGameEvents.Count))
            {
                if (_waitingGameEvents[i] != null)
                {
                    receivedAll = receivedAll && _waitingGameEvents[i].Received;
                }
                i++;
            }

            return receivedAll;
        }
    }
}
