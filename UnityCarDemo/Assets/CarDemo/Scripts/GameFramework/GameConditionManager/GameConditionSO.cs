using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "GameConditionSO", menuName = "Car Demo/SO/Game Condition/Game Condition")]

    public class GameConditionSO : ScriptableObject
    {
        public int IntValue = 0;
        public int BoolValue = 0;
        public float FloatValue = 0;
        public string StringValue = string.Empty;
    }
}
