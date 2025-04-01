using UnityEngine;
using UnityEngine.InputSystem;

namespace CarDemo
{
    public class GameInputManager : GameDirectorService
    {
        [SerializeField]
        private Key _forwardKey = Key.W;

        [SerializeField]
        private Key _reverseKey = Key.S;

        [SerializeField]
        private Key _leftKey = Key.A;

        [SerializeField]
        private Key _rightKey = Key.D;

        [SerializeField]
        private Key _handbrakeKey = Key.Space;

        bool _forwardTouch = false;
        bool _reverseTouch = false;
        bool _leftTouch = false;
        bool _rightTouch = false;
        bool _handbrakeTouch = false;

        public bool GetForward()
        {
            return Keyboard.current[_forwardKey].isPressed || _forwardTouch;
        }

        public bool GetReverse()
        {
            return Keyboard.current[_reverseKey].isPressed || _reverseTouch;
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

        public void SetForward(bool forward)
        {
            _forwardTouch = forward;
        }

        public void SetReverse(bool reverse)
        {
            _reverseTouch = reverse;
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
    }
}
