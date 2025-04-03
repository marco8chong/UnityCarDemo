using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "GameLoadingSceneSO", menuName = "Car Demo/SO/Game Loading/Game Loading Scene")]

    public class GameLoadingSceneSO : ScriptableObject
    {
        public string SceneName = string.Empty;
    }
}
