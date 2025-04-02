using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "StateMachineStateSO_PrintDebugMessage", menuName = "Car Demo/SO/Game State/Print Debug Message")]
    public class StateMachineStateSO_PrintDebugMessage : StateMachineStateSO
    {
        [SerializeField]
        private string _debugMessage = "Hello World";

        public override void StateStart(StateMachine stateMachine)
        {
            base.StateStart(stateMachine);

            Debug.Log(_debugMessage);
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
