using UnityEngine;

namespace CarDemo
{
    public class GameEventSO : ScriptableObject
    {
        public virtual string GetDebugMessage()
        {
            return this.name;
        }
    }
}