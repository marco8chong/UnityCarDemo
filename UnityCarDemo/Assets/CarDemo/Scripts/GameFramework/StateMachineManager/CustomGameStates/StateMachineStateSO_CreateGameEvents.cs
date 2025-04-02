using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "StateMachineStateSO_CreateGameEvents", menuName = "Car Demo/SO/Game State/Create Game Events")]
    public class StateMachineStateSO_CreateGameEvents : StateMachineStateSO
    {
        [SerializeField]
        private List<GameEventSO> _gameEvents = new List<GameEventSO>();

        public override void StateStart(StateMachine stateMachine)
        {
            base.StateStart(stateMachine);

            foreach (GameEventSO gameEventSO in _gameEvents)
            {
                if (_gameEvents != null)
                {
                    GameDirector.Instance.GameEventManager.CreateGameEvent(gameEventSO, this);
                }
            }
        }

        public override void StateTick()
        {
            base.StateTick();

            EndCurrentState();
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}
