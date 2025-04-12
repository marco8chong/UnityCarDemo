using System;
using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "CarWheelPhysicsSO", menuName = "Car Demo/SO/Car Physics/Car Wheel Physics")]
    public class CarWheelPhysicsSO : ScriptableObject
    {
        [Serializable]
        public struct SuspensionSpringSettings
        {
            public float Spring;
            public float Damper;
            public float TargetPosition;
        }

        [Serializable]
        public struct WheelFrictionSettings
        {
            public float ExtremumSlip;
            public float ExtremumValue;
            public float AsymptoteSlip;
            public float AsymptoteValue;
            public float Stiffness;
        }

        [SerializeField]
        private SuspensionSpringSettings _suspensionSpringSettings;

        [SerializeField]
        private WheelFrictionSettings _forwardFrictionSettings;

        [SerializeField]
        private WheelFrictionSettings _sidewaysFrictionSettings;

        public SuspensionSpringSettings SuspensionSpring
        {
            get
            {
                return _suspensionSpringSettings;
            }
            set
            {
                _suspensionSpringSettings = value;
            }
        }

        public WheelFrictionSettings ForwardFriction
        {
            get
            {
                return _forwardFrictionSettings;
            }
            set
            {
                _forwardFrictionSettings = value;
            }
        }

        public WheelFrictionSettings SidewaysFriction
        {
            get
            {
                return _sidewaysFrictionSettings;
            }
            set
            {
                _sidewaysFrictionSettings = value;
            }
        }
    }
}
