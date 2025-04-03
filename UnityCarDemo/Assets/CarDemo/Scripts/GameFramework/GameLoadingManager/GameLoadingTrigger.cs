using UnityEngine;

namespace CarDemo
{
    public class GameLoadingTrigger : MonoBehaviour
    {
        [SerializeField]
        GameLoadingSceneSO targetScene = null;

        public void LoadScene()
        {
            GameDirector.Instance.GameLoadingManager.LoadingSceneAsync(targetScene);
        }
    }
}
