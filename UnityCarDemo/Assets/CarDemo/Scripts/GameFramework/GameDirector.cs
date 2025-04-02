using NaughtyAttributes;
using UnityEngine;

namespace CarDemo
{
    public class GameDirector : MonoBehaviour
    {
        private static GameDirector _instance = null;

        [Header("Game Services\n")]

        [SerializeField]
        [Required("Game input manager required")]
        private GameInputManager _gameInputManager = null;

        [SerializeField]
        [Required("Camera manager required")]
        private CameraManager _cameraManager = null;

        public GameInputManager GameInputManager
        {
            get
            {
                return _gameInputManager;
            }
        }

        public CameraManager CameraManager
        {
            get
            {
                return _cameraManager;
            }
        }

        public static GameDirector Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }
    }
}
