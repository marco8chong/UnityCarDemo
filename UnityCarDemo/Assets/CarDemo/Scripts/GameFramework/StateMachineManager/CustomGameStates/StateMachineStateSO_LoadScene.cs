using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "StateMachineStateSO_LoadScene", menuName = "Car Demo/SO/Game State/Load Scene")]
    public class StateMachineStateSO_LoadScene : StateMachineStateSO
    {
        [SerializeField]
        private GameLoadingSceneSO _gameLoadingSceneSO;

        public override void StateStart(StateMachine stateMachine)
        {
            base.StateStart(stateMachine);

            if (_gameLoadingSceneSO)
            {
                GameDirector.Instance.GameLoadingManager.LoadingSceneAsync(_gameLoadingSceneSO);
            }

            EndCurrentState();
        }

        public override void StateTick()
        {
            base.StateTick();
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}
