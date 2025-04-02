using NaughtyAttributes;
using UnityEngine;

namespace CarDemo
{
    public class GameConditionTrigger : MonoBehaviour
    {
        [SerializeField]
        GameConditionSO _gameConditionSO = null;

        [Button]        
        public void RegisterGameCondition()
        {
            GameDirector.Instance.GameConditionManager.RegisterGameCondition(_gameConditionSO);
        }

        [Button]
        public void UnregisterGameCondition()
        {
            GameDirector.Instance.GameConditionManager.UnregisterGameCondition(_gameConditionSO);
        }

        public bool CheckCondition()
        {
            return GameDirector.Instance.GameConditionManager.CheckGameCondition(_gameConditionSO);
        }
    }
}
