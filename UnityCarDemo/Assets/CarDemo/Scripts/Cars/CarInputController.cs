using NaughtyAttributes;
using UnityEngine;

namespace CarDemo
{
    [RequireComponent(typeof(CarPhysicsController))]
    [RequireComponent(typeof(CarAddonController))]
    public class CarInputController : MonoBehaviour
    {
        [SerializeField]
        [Required("Car input settings required")]
        private CarInputSO _carInputSettings = null;

        private CarPhysicsController _carPhysicsController;
        private CarAddonController _carAddonController;

        private bool _currentHandbrake = false;
        private bool _lastHandbrake = false;

        private bool _currentAddon = false;
        private bool _lastAddon = false;

        private void Start()
        {
            _carPhysicsController = GetComponent<CarPhysicsController>();
            _carAddonController = GetComponent<CarAddonController>();
        }

        private void Update()
        {
            ProcessDigitalInput();
        }

        private void ProcessDigitalInput()
        {
            GameInputManager gameInputManager = GameDirector.Instance.GameInputManager;
            _currentHandbrake = gameInputManager.GetHandbrake();
            _currentAddon = gameInputManager.GetAddon();

            if (gameInputManager.GetThrottle())
            {
                _carPhysicsController.ThrottleAxis += Time.deltaTime / _carInputSettings.ThrottlePushingTime;
            }
            else
            {
                _carPhysicsController.ThrottleAxis -= Time.deltaTime / _carInputSettings.ThrottleReleasingTime;
            }

            if (gameInputManager.GetBrakeReverse())
            {
                _carPhysicsController.BrakeAxis += Time.deltaTime / _carInputSettings.BrakePushingTime;
            }
            else
            {
                _carPhysicsController.BrakeAxis -= Time.deltaTime / _carInputSettings.BrakeReleasingTime;
            }

            if (gameInputManager.GetLeft())
            {
                _carPhysicsController.SteeringAxis -= Time.deltaTime / _carInputSettings.SteeringTime;
            }

            if (gameInputManager.GetRight())
            {
                _carPhysicsController.SteeringAxis += Time.deltaTime / _carInputSettings.SteeringTime;
            }

            if (!(gameInputManager.GetLeft() || gameInputManager.GetRight()))
            {
                if (_carPhysicsController.SteeringAxis > 0.0f)
                {
                    _carPhysicsController.SteeringAxis = Mathf.Clamp(_carPhysicsController.SteeringAxis - Time.deltaTime / _carInputSettings.SteeringReturningTime, 0.0f, 1.0f);
                }
                else
                {
                    _carPhysicsController.SteeringAxis = Mathf.Clamp(_carPhysicsController.SteeringAxis + Time.deltaTime / _carInputSettings.SteeringReturningTime, -1.0f, 0.0f);
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
                    _carPhysicsController.HandbrakeAxis = Mathf.Clamp(_carPhysicsController.HandbrakeAxis - Time.deltaTime / _carInputSettings.HandbrakeReleasingTime, 0.5f, 1.0f);
                }
                else
                {
                    _carPhysicsController.HandbrakeAxis -= Time.deltaTime;
                }
            }

            if (_currentAddon && (!_lastAddon))
            {
                _carAddonController.TriggerAllAddons();
            }

            _lastHandbrake = _currentHandbrake;
            _lastAddon = _currentAddon;
        }
    }
}
