using System;
using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "CarPhysicsSO", menuName = "Car Demo/SO/Car Physics/Car Physics")]
    public class CarPhysicsSO : ScriptableObject
    {
        [SerializeField]
        [Range(20.0f, 350.0f)]
        private float _maxForwardSpeed = 250.0f;

        [SerializeField]
        [Range(10.0f, 120.0f)]
        private float _maxReverseSpeed = 45.0f;

        [SerializeField]
        [Range(1.0f, 20.0f)]
        private float _accelerationMultiplier = 10.0f;

        [SerializeField]
        [Range(1.0f, 10.0f)]
        private float _reverseMultiplier = 5.0f;

        [Space(10)]

        [SerializeField]
        [Range(10.0f, 45.0f)]
        private float _maxSteeringAngle = 27.0f;

        [Space(10)]

        [SerializeField]
        [Range(100.0f, 600.0f)]
        private float _brakeForce = 350.0f;

        [Space(10)]

        [SerializeField]
        private float _bodyMass = 1300.0f;

        [SerializeField]
        private Vector3 _bodyMassCenter;

        public float MaxForwardSpeed
        {
            get
            {
                return _maxForwardSpeed;
            }
        }

        public float MaxReverseSpeed
        {
            get
            {
                return _maxReverseSpeed;
            }
        }

        public float AccelerationMultiplier
        {
            get
            {
                return _accelerationMultiplier;
            }
        }

        public float ReverseMultiplier
        {
            get
            {
                return _reverseMultiplier;
            }
        }

        public float MaxSteeringAngle
        {
            get
            {
                return _maxSteeringAngle;
            }
        }

        public float BrakeForce
        {
            get
            {
                return _brakeForce;
            }
        }

        public float BodyMass
        {
            get
            {
                return _bodyMass;
            }
        }

        public Vector3 BodyMassCenter
        {
            get
            {
                return _bodyMassCenter;
            }
        }
    }
}
