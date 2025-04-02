using UnityEngine;

namespace CarDemo
{
    public abstract class StateMachineStateSO : ScriptableObject
    {
        [SerializeField]
        private StateMachineStateSO _nextState;

        private StateMachine _currentStateMachine = null;
        private float _stateTimeElapsed = 0.0f;
        private float _stateScaledTimeElapsed = 0.0f;

        public StateMachineStateSO NextState
        {
            get
            {
                return _nextState;
            }
        }

        public virtual void StateStart(StateMachine stateMachine)
        {
            _currentStateMachine = stateMachine;
        }

        public virtual void StateTick()
        {
            _stateTimeElapsed += Time.deltaTime;
            _stateScaledTimeElapsed += Time.timeScale * Time.deltaTime;
        }

        public virtual void StateEnd()
        {
        }

        public void EndCurrentState()
        {
            if (_currentStateMachine != null)
            {
                _currentStateMachine.NextGameState();
            }
        }
    }
}
