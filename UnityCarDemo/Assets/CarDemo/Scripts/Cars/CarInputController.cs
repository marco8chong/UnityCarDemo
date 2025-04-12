using UnityEngine;

namespace CarDemo
{
    [RequireComponent(typeof(CarPhysicsController))]
    public class CarInputController : MonoBehaviour
    {
        [SerializeField]
        [Range(0.1f, 3.0f)]
        private float _throttlePushingTime = 1.0f;

        [SerializeField]
        [Range(0.1f, 10.0f)]
        private float _throttleReleasingTime = 0.5f;

        [SerializeField]
        [Range(0.1f, 10.0f)]
        private float _brakePushingTime = 0.5f;

        [SerializeField]
        [Range(0.1f, 10.0f)]
        private float _BrakeReleasingTime = 0.5f;

        [SerializeField]
        [Range(0.1f, 10.0f)]
        private float _steeringTime = 0.5f;

        [SerializeField]
        [Range(0.1f, 1.0f)]
        private float _steeringReturningTime = 0.5f;

        [SerializeField]
        [Range(0.1f, 2.0f)]
        private float _handbrakeReleasingTime = 1.0f;

        private CarPhysicsController _carPhysicsController;

        private bool _currentHandbrake = false;
        private bool _lastHandbrake = false;

        private void Start()
        {
            _carPhysicsController = GetComponent<CarPhysicsController>();
        }

        private void Update()
        {
            ProcessDigitalInput();
        }

        private void ProcessDigitalInput()
        {
            GameInputManager gameInputManager = GameDirector.Instance.GameInputManager;
            _currentHandbrake = gameInputManager.GetHandbrake();

            if (gameInputManager.GetThrottle())
            {
                _carPhysicsController.ThrottleAxis += Time.deltaTime / _throttlePushingTime;
            }
            else
            {
                _carPhysicsController.ThrottleAxis -= Time.deltaTime / _throttleReleasingTime;
            }

            if (gameInputManager.GetBrakeReverse())
            {
                _carPhysicsController.BrakeAxis += Time.deltaTime / _brakePushingTime;
            }
            else
            {
                _carPhysicsController.BrakeAxis -= Time.deltaTime / _BrakeReleasingTime;
            }

            if (gameInputManager.GetLeft())
            {
                _carPhysicsController.SteeringAxis -= Time.deltaTime / _steeringTime;
            }

            if (gameInputManager.GetRight())
            {
                _carPhysicsController.SteeringAxis += Time.deltaTime / _steeringTime;
            }

            if (!(gameInputManager.GetLeft() || gameInputManager.GetRight()))
            {
                if (_carPhysicsController.SteeringAxis > 0.0f)
                {
                    _carPhysicsController.SteeringAxis = Mathf.Clamp(_carPhysicsController.SteeringAxis - Time.deltaTime / _steeringReturningTime, 0.0f, 1.0f);
                }
                else
                {
                    _carPhysicsController.SteeringAxis = Mathf.Clamp(_carPhysicsController.SteeringAxis + Time.deltaTime / _steeringReturningTime, -1.0f, 0.0f);
                }
            }

            if (_currentHandbrake && (!_lastHandbrake))
            {
                _carPhysicsController.HandbrakeAxis = 1.0f;
            }
            else
            {
                if (_currentHandbrake)
                {
                    _carPhysicsController.HandbrakeAxis = Mathf.Clamp(_carPhysicsController.HandbrakeAxis - Time.deltaTime / _handbrakeReleasingTime, 0.5f, 1.0f);
                }
                else
                {
                    _carPhysicsController.HandbrakeAxis -= Time.deltaTime;
                }
            }

            _lastHandbrake = _currentHandbrake;
        }
    }
}
