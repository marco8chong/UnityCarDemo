using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    public class GameConditionManager : GameDirectorService
    {
        public delegate void GameConditionAction(GameConditionSO gameConditionSO);

        public event GameConditionAction OnConditionRegistered;
        public event GameConditionAction OnConditionUnregistered;

        [SerializeField]
        private bool _printLog = true;

        private List<GameConditionSO> _gameConditions = new List<GameConditionSO>();

        public void RegisterGameCondition(GameConditionSO gameConditionSO)
        {
            if (gameConditionSO)
            {
                if (!_gameConditions.Contains(gameConditionSO))
                {
                    _gameConditions.Add(gameConditionSO);

                    OnConditionRegistered?.Invoke(gameConditionSO);

                    if (_printLog)
                    {
                        Debug.Log($"[GameConditionManager] Registered - {gameConditionSO.name}");
                    }
                }
            }
        }

        public void UnregisterGameCondition(GameConditionSO gameConditionSO)
        {
            if (gameConditionSO)
            {
                if (_gameConditions.Contains(gameConditionSO))
                {
                    _gameConditions.Remove(gameConditionSO);

                    OnConditionUnregistered?.Invoke(gameConditionSO);

                    if (_printLog)
                    {
                        Debug.Log($"[GameConditionManager] Unregistered - {gameConditionSO.name}");
                    }
                }
            }
        }

        public bool CheckGameCondition(GameConditionSO gameConditionSO)
        {
            return _gameConditions.Contains(gameConditionSO);
        }

        [Button]
        public void PrintGameConditions()
        {
            Debug.Log("Game Conditions:");
            Debug.Log("----- Start -----");

            for (int i = 0; i <  _gameConditions.Count; i++)
            {
                Debug.Log($"- {_gameConditions[i].name}");
            }

            Debug.Log("-----  END  -----");
        }
    }
}
