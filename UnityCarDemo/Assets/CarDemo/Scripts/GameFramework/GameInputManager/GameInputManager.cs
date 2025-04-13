using UnityEngine;
using UnityEngine.InputSystem;

namespace CarDemo
{
    public class GameInputManager : GameDirectorService
    {
        [SerializeField]
        private Key _throttleKey = Key.W;

        [SerializeField]
        private Key _brakeReverseKey = Key.S;

        [SerializeField]
        private Key _leftKey = Key.A;

        [SerializeField]
        private Key _rightKey = Key.D;

        [SerializeField]
        private Key _handbrakeKey = Key.Space;

        [SerializeField]
        private Key _addonKey = Key.E;

        bool _throttleTouch = false;
        bool _brakeReverseTouch = false;
        bool _leftTouch = false;
        bool _rightTouch = false;
        bool _handbrakeTouch = false;
        bool _addonTouch = false;

        public bool GetThrottle()
        {
            return Keyboard.current[_throttleKey].isPressed || _throttleTouch;
        }

        public bool GetBrakeReverse()
        {
            return Keyboard.current[_brakeReverseKey].isPressed || _brakeReverseTouch;
        }

        public bool GetLeft()
        {
            return Keyboard.current[_leftKey].isPressed || _leftTouch;
        }

        public bool GetRight()
        {
            return Keyboard.current[_rightKey].isPressed || _rightTouch;
        }

        public bool GetHandbrake()
        {
            return Keyboard.current[_handbrakeKey].isPressed || _handbrakeTouch;
        }

        public bool GetAddon()
        {
            return Keyboard.current[_addonKey].isPressed || _addonTouch;
        }

        public void SetThrottle(bool forward)
        {
            _throttleTouch = forward;
        }

        public void SetBrakeReverse(bool reverse)
        {
            _brakeReverseTouch = reverse;
        }

        public void SetLeft(bool left)
        {
            _leftTouch = left;
        }

        public void SetRight(bool right)
        {
            _rightTouch = right;
        }

        public void SetHandbrake(bool handbrake)
        {
            _handbrakeTouch = handbrake;
        }

        public void SetAddon(bool addOn)
        {
            _addonTouch = addOn;
        }
    }
}
