using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "StateMachineStateSO_WaitForGameCondition", menuName = "Car Demo/SO/Game State/Wait for Game Condition")]
    public class StateMachineStateSO_WaitForGameCondition : StateMachineStateSO
    {
        [SerializeField]
        private List<GameConditionSO> _gameConditions = new List<GameConditionSO>();

        public override void StateStart(StateMachine stateMachine)
        {
            base.StateStart(stateMachine);
        }

        public override void StateTick()
        {
            base.StateTick();

            if (CheckConditions())
            {
                EndCurrentState();
            }
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }

        private bool CheckConditions()
        {
            bool allPass = true;

            int i = 0;
            while (allPass && (i < _gameConditions.Count))
            {
                if (_gameConditions[i])
                {
                    allPass = allPass && GameDirector.Instance.GameConditionManager.CheckGameCondition(_gameConditions[i]);
                }

                i++;
            }

            return allPass;
        }
    }
}
