using NaughtyAttributes;
using System;
using UnityEngine;

namespace CarDemo
{
    public class CarPhysicsController : MonoBehaviour
    {
        //
        // Car Configuration
        //
        [Space(20)]
        [HorizontalLine(color: EColor.Blue)]
        [Header("Car Configuration")]     
        [Space(10)]

        [SerializeField]
        [Range(20, 350)]
        private int _maxSpeed = 180;

        [SerializeField]
        [Range(10, 120)]
        private int _maxReverseSpeed = 45;

        [SerializeField]
        [Range(1, 20)]
        private int _accelerationMultiplier = 2;

        [Space(10)]

        [SerializeField]
        [Range(10, 45)]
        private int _maxSteeringAngle = 27;

        [SerializeField]
        [Range(0.1f, 1f)]
        private float _steeringSpeed = 0.5f;
 
        [Space(10)]

        [SerializeField]
        [Range(100, 600)]
        private int _brakeForce = 350;

        [SerializeField]
        [Range(1, 10)]
        private int _decelerationMultiplier = 2;

        [SerializeField]
        [Range(1, 10)]
        private int _handbrakeDriftMultiplier = 5;

        [Space(10)]

        [SerializeField]
        private Vector3 _bodyMassCenter;

        //
        // Wheel Configuration
        //
        [Space(20)]
        [HorizontalLine(color: EColor.Blue)]
        [Header("Wheel Configuration")]
        [Space(10)]

        [Required("Front left mesh required")]
        public GameObject frontLeftMesh;
        [Required("Front left collider required")]
        public WheelCollider frontLeftCollider;
        [Space(10)]
        [Required("Front right mesh required")]
        public GameObject frontRightMesh;
        [Required("Front right collider required")]
        public WheelCollider frontRightCollider;
        [Space(10)]
        [Required("Rear left mesh required")]
        public GameObject rearLeftMesh;
        [Required("Rear left collider required")]
        public WheelCollider rearLeftCollider;
        [Space(10)]
        [Required("Rear right mesh required")]
        public GameObject rearRightMesh;
        [Required("Rear right collider required")]
        public WheelCollider rearRightCollider;

        /*
        Car Simulation 
        */
        private float _carSpeed;
        private bool _isDrifting;
        private bool _isTractionLocked;

        private Rigidbody _carRigidbody;
        private float _steeringAxis;
        private float _throttleAxis;
        private float _driftingAxis;
        private float _localVelocityZ;
        private float _localVelocityX;
        private bool _deceleratingCar;

        private WheelFrictionCurve _flWheelFriction;
        private float _flWextremumSlip;
        private WheelFrictionCurve _frWheelFriction;
        private float _frWextremumSlip;
        private WheelFrictionCurve _rlWheelFriction;
        private float _rlWextremumSlip;
        private WheelFrictionCurve _rrWheelFriction;
        private float _rrWextremumSlip;

        void Start()
        {
            _carRigidbody = gameObject.GetComponent<Rigidbody>();
            _carRigidbody.centerOfMass = _bodyMassCenter;

            _flWheelFriction = new WheelFrictionCurve();
            _flWheelFriction.extremumSlip = frontLeftCollider.sidewaysFriction.extremumSlip;
            _flWextremumSlip = frontLeftCollider.sidewaysFriction.extremumSlip;
            _flWheelFriction.extremumValue = frontLeftCollider.sidewaysFriction.extremumValue;
            _flWheelFriction.asymptoteSlip = frontLeftCollider.sidewaysFriction.asymptoteSlip;
            _flWheelFriction.asymptoteValue = frontLeftCollider.sidewaysFriction.asymptoteValue;
            _flWheelFriction.stiffness = frontLeftCollider.sidewaysFriction.stiffness;
            
            _frWheelFriction = new WheelFrictionCurve();
            _frWheelFriction.extremumSlip = frontRightCollider.sidewaysFriction.extremumSlip;
            _frWextremumSlip = frontRightCollider.sidewaysFriction.extremumSlip;
            _frWheelFriction.extremumValue = frontRightCollider.sidewaysFriction.extremumValue;
            _frWheelFriction.asymptoteSlip = frontRightCollider.sidewaysFriction.asymptoteSlip;
            _frWheelFriction.asymptoteValue = frontRightCollider.sidewaysFriction.asymptoteValue;
            _frWheelFriction.stiffness = frontRightCollider.sidewaysFriction.stiffness;
            
            _rlWheelFriction = new WheelFrictionCurve();
            _rlWheelFriction.extremumSlip = rearLeftCollider.sidewaysFriction.extremumSlip;
            _rlWextremumSlip = rearLeftCollider.sidewaysFriction.extremumSlip;
            _rlWheelFriction.extremumValue = rearLeftCollider.sidewaysFriction.extremumValue;
            _rlWheelFriction.asymptoteSlip = rearLeftCollider.sidewaysFriction.asymptoteSlip;
            _rlWheelFriction.asymptoteValue = rearLeftCollider.sidewaysFriction.asymptoteValue;
            _rlWheelFriction.stiffness = rearLeftCollider.sidewaysFriction.stiffness;
            
            _rrWheelFriction = new WheelFrictionCurve();
            _rrWheelFriction.extremumSlip = rearRightCollider.sidewaysFriction.extremumSlip;
            _rrWextremumSlip = rearRightCollider.sidewaysFriction.extremumSlip;
            _rrWheelFriction.extremumValue = rearRightCollider.sidewaysFriction.extremumValue;
            _rrWheelFriction.asymptoteSlip = rearRightCollider.sidewaysFriction.asymptoteSlip;
            _rrWheelFriction.asymptoteValue = rearRightCollider.sidewaysFriction.asymptoteValue;
            _rrWheelFriction.stiffness = rearRightCollider.sidewaysFriction.stiffness;
        }

        void Update()
        {
            _carSpeed = (2.0f * Mathf.PI * frontLeftCollider.radius * frontLeftCollider.rpm * 60.0f) / 1000.0f;
            _localVelocityX = transform.InverseTransformDirection(_carRigidbody.linearVelocity).x;
            _localVelocityZ = transform.InverseTransformDirection(_carRigidbody.linearVelocity).z;

            AnimateWheelMeshes();
        }

        public bool DeceleratingCar
        {
            get
            {
                return _deceleratingCar;
            }
            set
            {
                _deceleratingCar = value;
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
                _steeringAxis = value;
            }
        }

        public float CarSpeed
        {
            get
            {
                return _carSpeed;
            }
        }

        public bool IsDrifting
        {
            get
            {
                return _isDrifting;
            }
        }

        public bool IsTractionLocked
        {
            get
            {
                return _isTractionLocked;
            }
        }

        public void TurnLeft()
        {
            _steeringAxis = _steeringAxis - (Time.deltaTime * 10.0f * _steeringSpeed);

            if (_steeringAxis < -1.0f)
            {
                _steeringAxis = -1.0f;
            }

            float steeringAngle = _steeringAxis * _maxSteeringAngle;
            frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, _steeringSpeed);
            frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, _steeringSpeed);
        }

        public void TurnRight()
        {
            _steeringAxis = _steeringAxis + (Time.deltaTime * 10.0f * _steeringSpeed);
            
            if (_steeringAxis > 1.0f)
            {
                _steeringAxis = 1.0f;
            }

            float steeringAngle = _steeringAxis * _maxSteeringAngle;
            frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, _steeringSpeed);
            frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, _steeringSpeed);
        }

        public void ResetSteeringAngle()
        {
            if (_steeringAxis < 0.0f)
            {
                _steeringAxis = _steeringAxis + (Time.deltaTime * 10.0f * _steeringSpeed);
            }
            else
            {
                if (_steeringAxis > 0.0f)
                {
                    _steeringAxis = _steeringAxis - (Time.deltaTime * 10.0f * _steeringSpeed);
                }
            }

            if (Mathf.Abs(frontLeftCollider.steerAngle) < 1.0f)
            {
                _steeringAxis = 0.0f;
            }

            float steeringAngle = _steeringAxis * _maxSteeringAngle;
            frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, _steeringSpeed);
            frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, _steeringSpeed);
        }

        void AnimateWheelMeshes()
        {
            try
            {
                Quaternion _flWRotation;
                Vector3 _flWPosition;
                frontLeftCollider.GetWorldPose(out _flWPosition, out _flWRotation);
                frontLeftMesh.transform.position = _flWPosition;
                frontLeftMesh.transform.rotation = _flWRotation;

                Quaternion _frWRotation;
                Vector3 _frWPosition;
                frontRightCollider.GetWorldPose(out _frWPosition, out _frWRotation);
                frontRightMesh.transform.position = _frWPosition;
                frontRightMesh.transform.rotation = _frWRotation;

                Quaternion _rlWRotation;
                Vector3 _rlWPosition;
                rearLeftCollider.GetWorldPose(out _rlWPosition, out _rlWRotation);
                rearLeftMesh.transform.position = _rlWPosition;
                rearLeftMesh.transform.rotation = _rlWRotation;

                Quaternion _rrWRotation;
                Vector3 _rrWPosition;
                rearRightCollider.GetWorldPose(out _rrWPosition, out _rrWRotation);
                rearRightMesh.transform.position = _rrWPosition;
                rearRightMesh.transform.rotation = _rrWRotation;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }

        public void GoForward()
        {
            if (Mathf.Abs(_localVelocityX) > 2.5f)
            {
                _isDrifting = true;
            }
            else
            {
                _isDrifting = false;
            }

            _throttleAxis = _throttleAxis + (Time.deltaTime * 3.0f);
            if (_throttleAxis > 1.0f)
            {
                _throttleAxis = 1.0f;
            }

            if (_localVelocityZ < -1.0f)
            {
                Brakes();
            }
            else
            {
                if (Mathf.RoundToInt(_carSpeed) < _maxSpeed)
                {
                    frontLeftCollider.brakeTorque = 0.0f;
                    frontLeftCollider.motorTorque = (_accelerationMultiplier * 50.0f) * _throttleAxis;
                    frontRightCollider.brakeTorque = 0.0f;
                    frontRightCollider.motorTorque = (_accelerationMultiplier * 50.0f) * _throttleAxis;
                    rearLeftCollider.brakeTorque = 0.0f;
                    rearLeftCollider.motorTorque = (_accelerationMultiplier * 50.0f) * _throttleAxis;
                    rearRightCollider.brakeTorque = 0.0f;
                    rearRightCollider.motorTorque = (_accelerationMultiplier * 50.0f) * _throttleAxis;
                }
                else
                {
                    frontLeftCollider.motorTorque = 0.0f;
                    frontRightCollider.motorTorque = 0.0f;
                    rearLeftCollider.motorTorque = 0.0f;
                    rearRightCollider.motorTorque = 0.0f;
                }
            }
        }

        public void GoReverse()
        {
            if (Mathf.Abs(_localVelocityX) > 2.5f)
            {
                _isDrifting = true;
            }
            else
            {
                _isDrifting = false;
            }

            _throttleAxis = _throttleAxis - (Time.deltaTime * 3.0f);
            if (_throttleAxis < -1.0f)
            {
                _throttleAxis = -1.0f;
            }

            if (_localVelocityZ > 1.0f)
            {
                Brakes();
            }
            else
            {
                if (Mathf.Abs(Mathf.RoundToInt(_carSpeed)) < _maxReverseSpeed)
                {
                    frontLeftCollider.brakeTorque = 0.0f;
                    frontLeftCollider.motorTorque = (_accelerationMultiplier * 50.0f) * _throttleAxis;
                    frontRightCollider.brakeTorque = 0;
                    frontRightCollider.motorTorque = (_accelerationMultiplier * 50.0f) * _throttleAxis;
                    rearLeftCollider.brakeTorque = 0;
                    rearLeftCollider.motorTorque = (_accelerationMultiplier * 50.0f) * _throttleAxis;
                    rearRightCollider.brakeTorque = 0;
                    rearRightCollider.motorTorque = (_accelerationMultiplier * 50.0f) * _throttleAxis;
                }
                else
                {
                    frontLeftCollider.motorTorque = 0.0f;
                    frontRightCollider.motorTorque = 0.0f;
                    rearLeftCollider.motorTorque = 0.0f;
                    rearRightCollider.motorTorque = 0.0f;
                }
            }
        }

        public void ThrottleOff()
        {
            frontLeftCollider.motorTorque = 0.0f;
            frontRightCollider.motorTorque = 0.0f;
            rearLeftCollider.motorTorque = 0.0f;
            rearRightCollider.motorTorque = 0.0f;
        }

        public void DecelerateCar()
        {
            if (Mathf.Abs(_localVelocityX) > 2.5f)
            {
                _isDrifting = true;
            }
            else
            {
                _isDrifting = false;
            }

            if (_throttleAxis != 0.0f)
            {
                if (_throttleAxis > 0.0f)
                {
                    _throttleAxis = _throttleAxis - (Time.deltaTime * 10.0f);
                }
                else
                {
                    if (_throttleAxis < 0.0f)
                    {
                        _throttleAxis = _throttleAxis + (Time.deltaTime * 10.0f);
                    }
                }

                if (Mathf.Abs(_throttleAxis) < 0.15f)
                {
                    _throttleAxis = 0.0f;
                }
            }

            _carRigidbody.linearVelocity = _carRigidbody.linearVelocity * (1.0f / (1.0f + (0.025f * _decelerationMultiplier)));

            frontLeftCollider.motorTorque = 0.0f;
            frontRightCollider.motorTorque = 0.0f;
            rearLeftCollider.motorTorque = 0.0f;
            rearRightCollider.motorTorque = 0.0f;

            if (_carRigidbody.linearVelocity.magnitude < 0.25f)
            {
                _carRigidbody.linearVelocity = Vector3.zero;
                CancelInvoke("DecelerateCar");
            }
        }

        public void Brakes()
        {
            frontLeftCollider.brakeTorque = _brakeForce;
            frontRightCollider.brakeTorque = _brakeForce;
            rearLeftCollider.brakeTorque = _brakeForce;
            rearRightCollider.brakeTorque = _brakeForce;
        }

        public void Handbrake()
        {
            CancelInvoke("RecoverTraction");

            _driftingAxis = _driftingAxis + (Time.deltaTime);
            float secureStartingPoint = _driftingAxis * _flWextremumSlip * _handbrakeDriftMultiplier;

            if (secureStartingPoint < _flWextremumSlip)
            {
                _driftingAxis = _flWextremumSlip / (_flWextremumSlip * _handbrakeDriftMultiplier);
            }

            if (_driftingAxis > 1.0f)
            {
                _driftingAxis = 1.0f;
            }

            if (Mathf.Abs(_localVelocityX) > 2.5f)
            {
                _isDrifting = true;
            }
            else
            {
                _isDrifting = false;
            }

            if (_driftingAxis < 1.0f)
            {
                _flWheelFriction.extremumSlip = _flWextremumSlip * _handbrakeDriftMultiplier * _driftingAxis;
                frontLeftCollider.sidewaysFriction = _flWheelFriction;

                _frWheelFriction.extremumSlip = _frWextremumSlip * _handbrakeDriftMultiplier * _driftingAxis;
                frontRightCollider.sidewaysFriction = _frWheelFriction;

                _rlWheelFriction.extremumSlip = _rlWextremumSlip * _handbrakeDriftMultiplier * _driftingAxis;
                rearLeftCollider.sidewaysFriction = _rlWheelFriction;

                _rrWheelFriction.extremumSlip = _rrWextremumSlip * _handbrakeDriftMultiplier * _driftingAxis;
                rearRightCollider.sidewaysFriction = _rrWheelFriction;
            }

            _isTractionLocked = true;
        }

        public void RecoverTraction()
        {
            _isTractionLocked = false;
            _driftingAxis = _driftingAxis - (Time.deltaTime / 1.5f);

            if (_driftingAxis < 0.0f)
            {
                _driftingAxis = 0.0f;
            }

            if (_flWheelFriction.extremumSlip > _flWextremumSlip)
            {
                _flWheelFriction.extremumSlip = _flWextremumSlip * _handbrakeDriftMultiplier * _driftingAxis;
                frontLeftCollider.sidewaysFriction = _flWheelFriction;

                _frWheelFriction.extremumSlip = _frWextremumSlip * _handbrakeDriftMultiplier * _driftingAxis;
                frontRightCollider.sidewaysFriction = _frWheelFriction;

                _rlWheelFriction.extremumSlip = _rlWextremumSlip * _handbrakeDriftMultiplier * _driftingAxis;
                rearLeftCollider.sidewaysFriction = _rlWheelFriction;

                _rrWheelFriction.extremumSlip = _rrWextremumSlip * _handbrakeDriftMultiplier * _driftingAxis;
                rearRightCollider.sidewaysFriction = _rrWheelFriction;

                Invoke("RecoverTraction", Time.deltaTime);
            }
            else
            {
                if (_flWheelFriction.extremumSlip < _flWextremumSlip)
                {
                    _flWheelFriction.extremumSlip = _flWextremumSlip;
                    frontLeftCollider.sidewaysFriction = _flWheelFriction;

                    _frWheelFriction.extremumSlip = _frWextremumSlip;
                    frontRightCollider.sidewaysFriction = _frWheelFriction;

                    _rlWheelFriction.extremumSlip = _rlWextremumSlip;
                    rearLeftCollider.sidewaysFriction = _rlWheelFriction;

                    _rrWheelFriction.extremumSlip = _rrWextremumSlip;
                    rearRightCollider.sidewaysFriction = _rrWheelFriction;

                    _driftingAxis = 0f;
                }
            }
        }
    }
}
