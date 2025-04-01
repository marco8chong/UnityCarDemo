using UnityEngine;

namespace CarDemo
{
    public class GameInputManager : GameDirectorService
    {
        [SerializeField]
        private KeyCode _forwardKey = KeyCode.W;

        [SerializeField]
        private KeyCode _reverseKey = KeyCode.S;

        [SerializeField]
        private KeyCode _leftKey = KeyCode.A;

        [SerializeField]
        private KeyCode _rightKey = KeyCode.D;

        [SerializeField]
        private KeyCode _handbrakeKey = KeyCode.Space;

        bool _forwardTouch = false;
        bool _reverseTouch = false;
        bool _leftTouch = false;
        bool _rightTouch = false;
        bool _handbrakeTouch = false;

        public bool GetForward()
        {
            return Input.GetKey(_forwardKey) || _forwardTouch;
        }

        public bool GetReverse()
        {
            return Input.GetKey(_reverseKey) || _reverseTouch;
        }

        public bool GetLeft()
        {
            return Input.GetKey(_leftKey) || _leftTouch;
        }

        public bool GetRight()
        {
            return Input.GetKey(_rightKey) || _rightTouch;
        }

        public bool GetHandbrake()
        {
            return Input.GetKey(_handbrakeKey) || _handbrakeTouch;
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
