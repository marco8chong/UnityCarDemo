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

        [SerializeField]
        [Required("Object tag manager required")]
        private ObjectTagManager _objectTagManager = null;

        [SerializeField]
        [Required("Game event manager required")]
        private GameEventManager _gameEventManager = null;

        [SerializeField]
        [Required("Game condition manager required")]
        private GameConditionManager _gameConditionManager = null;

        [SerializeField]
        [Required("State machine manager required")]
        private StateMachineManager _stateMachineManager = null;

        [SerializeField]
        [Required("Game loading manager required")]
        private GameLoadingManager _gameLoadingManager = null;

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

        public ObjectTagManager ObjectTagManager
        {
            get
            {
                return _objectTagManager;
            }
        }

        public GameEventManager GameEventManager
        {
            get
            {
                return _gameEventManager;
            }
        }

        public GameConditionManager GameConditionManager
        {
            get
            {
                return _gameConditionManager;
            }
        }

        public StateMachineManager StateMachineManager
        {
            get
            {
                return _stateMachineManager;
            }
        }

        public GameLoadingManager GameLoadingManager
        {
            get
            {
                return _gameLoadingManager;
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
