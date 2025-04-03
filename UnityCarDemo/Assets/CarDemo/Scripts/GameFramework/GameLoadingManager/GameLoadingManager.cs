using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarDemo
{
    public class GameLoadingManager : GameDirectorService
    {
        public void LoadingSceneAsync(GameLoadingSceneSO scene)
        {
            StartCoroutine(LoadSceneAsync(scene));
        }

        private IEnumerator LoadSceneAsync(GameLoadingSceneSO scene)
        {
            if (scene)
            {
                GameLoadingScreen loadingScreen = FindFirstObjectByType<GameLoadingScreen>(FindObjectsInactive.Include);
                loadingScreen?.Show();
                loadingScreen?.SetText("0%");

                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.SceneName);
                asyncLoad.allowSceneActivation = false;

                while (!asyncLoad.isDone)
                {
                    string progressStr = $"{(int)(asyncLoad.progress * 100.0f)}%";
                    Debug.Log($"Scene Loading Progress: {scene.SceneName} - {progressStr}");
                    loadingScreen?.SetText(progressStr);

                    if (asyncLoad.progress >= 0.9f)
                    {
                        asyncLoad.allowSceneActivation = true;
                    }

                    yield return null;
                }
            }
        }
    }
}
