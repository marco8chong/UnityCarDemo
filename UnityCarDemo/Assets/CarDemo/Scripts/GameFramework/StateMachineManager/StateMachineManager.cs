using NaughtyAttributes;
using System.Collections.Generic;

namespace CarDemo
{
    public class StateMachineManager : GameDirectorService
    {
        private List<StateMachine> _registeredStateMachines = new List<StateMachine>();

        [ShowNativeProperty]
        public int RegisteredStateMachineCount => _registeredStateMachines.Count;

        public List<StateMachine> RegisteredStateMachines
        {
            get
            {
                return _registeredStateMachines;
            }
        } 

        public void RegisterStateMachine(StateMachine stateMachine)
        {
            if (stateMachine != null)
            {
                _registeredStateMachines.Add(stateMachine);
            }
        }

        public void UnregisterStateMachine(StateMachine stateMachine)
        {
            if (stateMachine != null)
            {
                _registeredStateMachines.Remove(stateMachine);
            }
        }
    }
}