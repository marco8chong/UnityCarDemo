using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "StateMachineStateSO_WaitForTime", menuName = "Car Demo/SO/Game State/Wait for Time")]
    public class StateMachineStateSO_WaitForTime : StateMachineStateSO
    {
        public enum WaitingMode { RealTime, ScaledTime }

        [SerializeField]
        private WaitingMode _waitingMode = WaitingMode.RealTime;

        [SerializeField]
        private float _waitingTime = 1.0f;

        private float _timeElapsed = 0.0f; 

        public override void StateStart(StateMachine stateMachine)
        {
            base.StateStart(stateMachine);

            _timeElapsed = 0.0f;
        }

        public override void StateTick()
        {
            base.StateTick();

            switch (_waitingMode)
            {
                case WaitingMode.RealTime:
                    {
                        _timeElapsed += Time.deltaTime;
                    }
                    break;
                case WaitingMode.ScaledTime:
                    {
                        _timeElapsed += Time.deltaTime * Time.timeScale;
                    }
                    break;
            }

            if (_timeElapsed >= _waitingTime)
            {
                EndCurrentState();
            }
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}
