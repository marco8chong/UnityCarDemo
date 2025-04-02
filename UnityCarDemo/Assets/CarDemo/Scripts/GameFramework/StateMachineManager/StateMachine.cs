using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField]
        [Required("Initial game state required")]
        private StateMachineStateSO _initialState;

        [SerializeField]
        private bool _autoStart = true;

        [SerializeField]
        private bool _printStartLog = true;

        [SerializeField]
        private bool _printEndLog = true;
        
        private StateMachineStateSO _currentState;

        [ShowNonSerializedField]
        private string _currentStateName = "";

        [SerializeField]
        private List<string> _debugHistory = new List<string>();

        private void Start()
        {
            GameDirector.Instance.StateMachineManager.RegisteredStateMachines.Add(this);

            if (_autoStart)
            {
                StartInitialState();
            }

            _debugHistory.Clear();
        }

        private void OnDestroy()
        {
            GameDirector.Instance.StateMachineManager.UnregisterStateMachine(this);
        }

        private void Update()
        {
            if (_currentState != null)
            {
                _currentState.StateTick();
            }
        }

        public void StartInitialState()
        {
            if (_initialState != null)
            {
                LoadGameState(_initialState);
            }
        }

        public void LoadGameState(StateMachineStateSO gameState)
        {
            if (_currentState != null)
            {
                _currentState.StateEnd();

                if (_printEndLog)
                {
                    Debug.Log($"[StateMachine {this.name}] State {_currentState.name} ended.");
                }
            }

            _currentState = null;
            
            if (gameState)
            {
                _currentState = Object.Instantiate(gameState);
            }

            if (_currentState != null)
            {
                if (_printStartLog)
                {
                    Debug.Log($"[StateMachine {this.name}] State {_currentState.name} has started.");
                }

                _currentStateName = _currentState.name;
                
                if (Application.isEditor)
                {
                    _debugHistory.Add(_currentStateName);
                }

                _currentState.StateStart(this);
            }
            else
            {
                _currentStateName = "";
            }
        }

        public void NextGameState()
        {
            if (_currentState != null)
            {
                LoadGameState(_currentState.NextState);
            }
        }
    }
}