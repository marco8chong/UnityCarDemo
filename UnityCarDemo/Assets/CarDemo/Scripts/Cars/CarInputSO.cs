using System;
using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "CarInputSO", menuName = "Car Demo/SO/Car Input/Car Input")]
    public class CarInputSO : ScriptableObject
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
        private float _brakeReleasingTime = 0.5f;

        [SerializeField]
        [Range(0.1f, 10.0f)]
        private float _steeringTime = 0.5f;

        [SerializeField]
        [Range(0.1f, 1.0f)]
        private float _steeringReturningTime = 0.5f;

        [SerializeField]
        [Range(0.1f, 2.0f)]
        private float _handbrakeReleasingTime = 1.0f;

        public float ThrottlePushingTime
        {
            get
            {
                return _throttlePushingTime;
            }
            set
            {
                _throttlePushingTime = value;
            }
        }

        public float ThrottleReleasingTime
        {
            get
            {
                return _throttleReleasingTime;
            }
            set
            {
                _throttleReleasingTime = value;
            }
        }

        public float BrakePushingTime
        {
            get
            {
                return _brakePushingTime;
            }
            set
            {
                _brakePushingTime = value;
            }
        }

        public float BrakeReleasingTime
        {
            get
            {
                return _brakeReleasingTime;
            }
            set
            {
                _brakeReleasingTime = value;
            }
        }

        public float SteeringTime
        {
            get
            {
                return _steeringTime;
            }
            set
            {
                _steeringTime = value;
            }
        }

        public float SteeringReturningTime
        {
            get
            {
                return _steeringReturningTime;
            }
            set
            {
                _steeringReturningTime = value;
            }
        }

        public float HandbrakeReleasingTime
        {
            get
            {
                return _handbrakeReleasingTime;
            }
            set
            {
                _handbrakeReleasingTime = value;
            }
        }
    }
}
