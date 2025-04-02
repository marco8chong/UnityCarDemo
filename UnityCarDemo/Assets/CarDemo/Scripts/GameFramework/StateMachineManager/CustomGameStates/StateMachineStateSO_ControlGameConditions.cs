using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "StateMachineStateSO_ControlGameConditions", menuName = "Car Demo/SO/Game State/Control Game Conditions")]
    public class StateMachineStateSO_ControlGameConditions : StateMachineStateSO
    {
        [SerializeField]
        private List<GameConditionSO> _registerGameConditions = new List<GameConditionSO>();

        [SerializeField]
        private List<GameConditionSO> _unregisterGameConditions = new List<GameConditionSO>();

        public override void StateStart(StateMachine stateMachine)
        {
            base.StateStart(stateMachine);

            ControlGameConditions();
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

        private void ControlGameConditions()
        {
            foreach (GameConditionSO gameConditionSO in _registerGameConditions)
            {
                GameDirector.Instance.GameConditionManager.RegisterGameCondition(gameConditionSO);
            }

            foreach (GameConditionSO gameConditionSO in _unregisterGameConditions)
            {
                GameDirector.Instance.GameConditionManager.UnregisterGameCondition(gameConditionSO);
            }
        }
    }
}
