using NaughtyAttributes;
using System;
using UnityEngine;

namespace CarDemo
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarPhysicsController : MonoBehaviour
    {
        public event Action OnCarPhysicsChanged;

        // car configuration
        [Header("Car Physics Configuration")]
        [Space(10)]

        [SerializeField]
        [Required("Car physics settings required")]
        private CarPhysicsSO _carPhysicsSettings = null;

        [SerializeField]
        [Required("Wheel physics settings required (normal)")]
        private CarWheelPhysicsSO _carWheelPhysicsSettingsNormal = null;

        [SerializeField]
        [Required("Wheel physics settings required (drift)")]
        private CarWheelPhysicsSO _carWheelPhysicsSettingsDrift = null;

        // wheel configuration
        [Space(20)]
        [Header("Wheel Configuration")]
        [Space(10)]

        [SerializeField]
        [Required("Front left wheel required")]
        private CarWheelPhysics _frontLeftWheel;
        [Space(10)]
        [SerializeField]
        [Required("Front right wheel required")]
        private CarWheelPhysics _frontRightWheel;
        [Space(10)]
        [SerializeField]
        [Required("Rear left wheel required")]
        private CarWheelPhysics _rearLeftWheel;
        [Space(10)]
        [SerializeField]
        [Required("Rear right wheel required")]
        private CarWheelPhysics _rearRightWheel;

        // effects
        [Space(20)]
        [Header("Effects")]
        [Space(10)]

        [SerializeField]
        private bool _useEffects = true;

        [SerializeField]
        [Required("Rear left particle system required")]
        private ParticleSystem _rlParticleSystem;

        [SerializeField]
        [Required("Rear right particle system required")]
        private ParticleSystem _rrParticleSystem;

        // car simulation 
        private Rigidbody _carRigidbody = null;

        private float _carSpeed = 0.0f;
        private float _localVelocityZ = 0.0f;
        private float _localVelocityX = 0.0f;

        private float _steeringAxis = 0.0f;
        private float _throttleAxis = 0.0f;
        private float _brakeAxis = 0.0f;
        private float _handbrakeAxis = 0.0f;

        void Start()
        {
            _carRigidbody = gameObject.GetComponent<Rigidbody>();
            
            ApplyPhysicsSettings();

            DriftParicle(false);
        }

        void Update()
        {
            _carSpeed = (2.0f * Mathf.PI * _frontLeftWheel.WheelRadius * _frontLeftWheel.WheelRpm * 60.0f) / 1000.0f;
            _localVelocityX = transform.InverseTransformDirection(_carRigidbody.linearVelocity).x;
            _localVelocityZ = transform.InverseTransformDirection(_carRigidbody.linearVelocity).z;

            RunCarPhysics();
            DriftParicle(Mathf.Abs(_localVelocityX) > 2.5f);
        }

        public CarPhysicsSO CarPhysicsSettings
        {
            get
            {
                return _carPhysicsSettings;
            }
            set
            {
                _carPhysicsSettings = value;
                ApplyPhysicsSettings();
                OnCarPhysicsChanged?.Invoke();
            }
        }

        public CarWheelPhysicsSO CarWheelPhysicsSettingsNormal
        {
            get
            {
                return _carWheelPhysicsSettingsNormal;
            }
            set
            {
                _carWheelPhysicsSettingsNormal = value;
                OnCarPhysicsChanged?.Invoke();
            }
        }

        public CarWheelPhysicsSO CarWheelPhysicsSettingDrift
        {
            get
            {
                return _carWheelPhysicsSettingsDrift;
            }
            set
            {
                _carWheelPhysicsSettingsDrift = value;
                OnCarPhysicsChanged?.Invoke();
            }
        }

        public float CarSpeed
        {
            get
            {
                return _carSpeed;
            }
        }

        public float SteeringAxis
        {
            get
            {
                return _steeringAxis;
            }
            set
            {
                _steeringAxis = Mathf.Clamp(value, -1.0f, 1.0f);
            }
        }

        public float ThrottleAxis
        {
            get
            {
                return _throttleAxis;
            }
            set
            {
                _throttleAxis = Mathf.Clamp01(value);
            }
        }

        public float BrakeAxis
        {
            get
            {
                return _brakeAxis;
            }
            set
            {
                _brakeAxis = Mathf.Clamp01(value);
            }
        }

        public float HandbrakeAxis
        {
            get
            {
                return _handbrakeAxis;
            }
            set
            {
                _handbrakeAxis = Mathf.Clamp01(value);
            }
        }

        public void RunCarPhysics()
        {
            if (_brakeAxis > 0.0f)
            {
                if (_localVelocityZ > 0.0f)
                {
                    // brake
                    Brake(true);
                }
                else
                {
                    // reverse
                    if (Mathf.RoundToInt(_carSpeed) < _carPhysicsSettings.MaxReverseSpeed)
                    {
                        AccelerateBackward();
                    }
                    else
                    {
                        MaintainSpeed();
                    }
                }
            }
            else
            {
                if (_localVelocityZ < 0.0f)
                {
                    // brake
                    Brake(false);
                }
                else
                {
                    // go forward
                    if (Mathf.RoundToInt(_carSpeed) < _carPhysicsSettings.MaxForwardSpeed)
                    {
                        AccelerateForward();
                    }
                    else
                    {
                        MaintainSpeed();
                    }
                }
            }

            Turn();
        }

        private void AccelerateForward()
        {
            _frontLeftWheel.BrakeTorque = 0.0f;
            _frontLeftWheel.MotorTorque = (_carPhysicsSettings.AccelerationMultiplier * 50.0f) * _throttleAxis;
            _frontRightWheel.BrakeTorque = 0.0f;
            _frontRightWheel.MotorTorque = (_carPhysicsSettings.AccelerationMultiplier * 50.0f) * _throttleAxis;
            _rearLeftWheel.BrakeTorque = 0.0f;
            _rearLeftWheel.MotorTorque = (_carPhysicsSettings.AccelerationMultiplier * 50.0f) * _throttleAxis;
            _rearRightWheel.BrakeTorque = 0.0f;
            _rearRightWheel.MotorTorque = (_carPhysicsSettings.AccelerationMultiplier * 50.0f) * _throttleAxis;
        }

        private void AccelerateBackward()
        {
            _frontLeftWheel.BrakeTorque = 0.0f;
            _frontLeftWheel.MotorTorque = (_carPhysicsSettings.ReverseMultiplier * 50.0f) * -_brakeAxis;
            _frontRightWheel.BrakeTorque = 0;
            _frontRightWheel.MotorTorque = (_carPhysicsSettings.ReverseMultiplier * 50.0f) * -_brakeAxis;
            _rearLeftWheel.BrakeTorque = 0;
            _rearLeftWheel.MotorTorque = (_carPhysicsSettings.ReverseMultiplier * 50.0f) * -_brakeAxis;
            _rearRightWheel.BrakeTorque = 0;
            _rearRightWheel.MotorTorque = (_carPhysicsSettings.ReverseMultiplier * 50.0f) * -_brakeAxis;
        }

        private void MaintainSpeed()
        {
            _frontLeftWheel.BrakeTorque = 0.0f;
            _frontLeftWheel.MotorTorque = 0.0f;
            _frontRightWheel.BrakeTorque = 0.0f;
            _frontRightWheel.MotorTorque = 0.0f;
            _rearLeftWheel.BrakeTorque = 0.0f;
            _rearLeftWheel.MotorTorque = 0.0f;
            _rearRightWheel.BrakeTorque = 0.0f;
            _rearRightWheel.MotorTorque = 0.0f;
        }

        private void Brake(bool useBrakeAxis = true)
        {
            float brakeForce = useBrakeAxis ? _carPhysicsSettings.BrakeForce * _brakeAxis : _carPhysicsSettings.BrakeForce;

            _frontLeftWheel.BrakeTorque = brakeForce;
            _frontLeftWheel.MotorTorque = 0.0f;
            _frontRightWheel.BrakeTorque = brakeForce;
            _frontRightWheel.MotorTorque = 0.0f;
            _rearLeftWheel.BrakeTorque = brakeForce;
            _rearLeftWheel.MotorTorque = 0.0f;
            _rearRightWheel.BrakeTorque = brakeForce;
            _rearRightWheel.MotorTorque = 0.0f;
        }

        private void Turn()
        {
            float steeringAngle = _steeringAxis * _carPhysicsSettings.MaxSteeringAngle;
            _frontLeftWheel.SteerAngle = steeringAngle;
            _frontRightWheel.SteerAngle = steeringAngle;
        }

        private void DriftParicle(bool emitParticle)
        {
            if (_rlParticleSystem && _rrParticleSystem)
            {
                if (_useEffects && emitParticle)
                {
                    _rlParticleSystem.Play();
                    _rrParticleSystem.Play();
                }
                else
                {
                    _rlParticleSystem.Stop();
                    _rrParticleSystem.Stop();
                }
            }
        }

        private void ApplyPhysicsSettings()
        {
            _carRigidbody.mass = _carPhysicsSettings.BodyMass;
            _carRigidbody.centerOfMass = _carPhysicsSettings.BodyMassCenter;
        }

        [Button]
        public void ForceUpdatePhysicsSettings()
        {
            ApplyPhysicsSettings();

            OnCarPhysicsChanged?.Invoke();
        }
    }
}
