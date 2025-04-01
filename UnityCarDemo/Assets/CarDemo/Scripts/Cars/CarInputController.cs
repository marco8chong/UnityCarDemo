using UnityEngine;

namespace CarDemo
{
    [RequireComponent(typeof(CarPhysicsController))]
    public class CarInputController : MonoBehaviour
    {
        private CarPhysicsController _carPhysicsController;

        private bool _currentForward = false;
        private bool _lastForward = false;

        private bool _currentReverse = false;
        private bool _lastReverse = false;

        private bool _currentLeft = false;
        private bool _lastLeft = false;

        private bool _currentRight = false;
        private bool _lastRight = false;

        private bool _currentHandbrake = false;
        private bool _lastHandbrake = false;

        private void Start()
        {
            _carPhysicsController = GetComponent<CarPhysicsController>();
        }

        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            GameInputManager gameInputManager = GameDirector.Instance.GameInputManager;
            _currentForward = gameInputManager.GetForward();
            _currentReverse = gameInputManager.GetReverse();
            _currentLeft = gameInputManager.GetLeft();
            _currentRight = gameInputManager.GetRight();
            _currentHandbrake = gameInputManager.GetHandbrake();

            if (_currentForward)
            {
                CancelInvoke("DecelerateCar");
                _carPhysicsController.DeceleratingCar = false;
                _carPhysicsController.GoForward();
            }
            if (_currentReverse)
            {
                CancelInvoke("DecelerateCar");
                _carPhysicsController.DeceleratingCar = false;
                _carPhysicsController.GoReverse();
            }

            if (_currentLeft)
            {
                _carPhysicsController.TurnLeft();
            }

            if (_currentRight)
            {
                _carPhysicsController.TurnRight();
            }

            if (_currentHandbrake)
            {
                CancelInvoke("DecelerateCar");
                _carPhysicsController.DeceleratingCar = false;
                _carPhysicsController.Handbrake();
            }

            if (_lastHandbrake && (!_currentHandbrake))
            {
                _carPhysicsController.RecoverTraction();
            }

            if ((!_currentReverse && !_currentForward))
            {
                _carPhysicsController.ThrottleOff();
            }

            if ((!_currentReverse && !_currentForward) && !_currentHandbrake && !_carPhysicsController.DeceleratingCar)
            {
                InvokeRepeating("DecelerateCar", 0.0f, 0.1f);
                _carPhysicsController.DeceleratingCar = true;
            }

            if (!_currentLeft && !_currentRight && _carPhysicsController.SteeringAxis != 0.0f)
            {
                _carPhysicsController.ResetSteeringAngle();
            }

            _lastForward = _currentForward;
            _lastReverse = _currentReverse;
            _lastLeft = _currentLeft;
            _lastRight = _currentRight;
            _lastHandbrake = _currentHandbrake;
        }

        private void DecelerateCar()
        {
            _carPhysicsController.DecelerateCar();
        }
    }
}
