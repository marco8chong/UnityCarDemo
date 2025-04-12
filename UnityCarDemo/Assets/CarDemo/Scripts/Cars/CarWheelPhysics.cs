using NaughtyAttributes;
using UnityEngine;

namespace CarDemo
{
    [RequireComponent(typeof(WheelCollider))]
    public class CarWheelPhysics : MonoBehaviour
    {
        [SerializeField]
        [Required("Wheel physics settings required (normal)")]
        private CarWheelPhysicsSO _carWheelPhysicsSettingsNormal;

        [SerializeField]
        [Required("Wheel physics settings required (drift)")]
        private CarWheelPhysicsSO _carWheelPhysicsSettingsDrift;

        [SerializeField]
        private Transform _wheelMesh = null;

        private WheelCollider _wheelCollider = null;
        private CarPhysicsController _carPhysicsController = null;

        private JointSpring _suspensionSpring = new JointSpring();
        private WheelFrictionCurve _forwardFriction = new WheelFrictionCurve();
        private WheelFrictionCurve _sidewaysFriction = new WheelFrictionCurve();

        private float _lastHandbrakeAxis = 0.0f;

        public float WheelRadius
        {
            get
            {
                return _wheelCollider.radius;
            }
        }

        public float WheelRpm
        {
            get
            {
                return _wheelCollider.rpm;
            }
        }

        public float SteerAngle
        {
            get
            {
                return _wheelCollider.steerAngle;
            }
            set
            {
                _wheelCollider.steerAngle = value;
            }
        }

        public float MotorTorque
        {
            get
            {
                return _wheelCollider.motorTorque;
            }
            set
            {
                _wheelCollider.motorTorque = value;
            }
        }

        public float BrakeTorque
        {
            get
            {
                return _wheelCollider.brakeTorque;
            }
            set
            {
                _wheelCollider.brakeTorque = value;
            }
        }

        private void Start()
        {
            _wheelCollider = GetComponent<WheelCollider>();
            _carPhysicsController = GetComponentInParent<CarPhysicsController>();

            UpdateWheelParameters(true);
        }

        private void Update()
        {
            UpdateWheelParameters();
            AnimateWheelMesh();
        }

        private void UpdateWheelParameters(bool forceUpdate = false)
        {
            if (_carPhysicsController && _carWheelPhysicsSettingsNormal && _carWheelPhysicsSettingsDrift)
            {
                float currentHandbrakeAxis = _carPhysicsController.HandbrakeAxis;

                if (forceUpdate || (currentHandbrakeAxis != _lastHandbrakeAxis))
                {
                    float normalRatio = 1.0f - currentHandbrakeAxis;
                    float driftRatio = currentHandbrakeAxis;

                    _suspensionSpring.spring = _carWheelPhysicsSettingsNormal.SuspensionSpring.Spring * normalRatio + _carWheelPhysicsSettingsDrift.SuspensionSpring.Spring * driftRatio;
                    _suspensionSpring.damper = _carWheelPhysicsSettingsNormal.SuspensionSpring.Damper * normalRatio + _carWheelPhysicsSettingsDrift.SuspensionSpring.Damper * driftRatio;
                    _suspensionSpring.targetPosition = _carWheelPhysicsSettingsNormal.SuspensionSpring.TargetPosition * normalRatio + _carWheelPhysicsSettingsDrift.SuspensionSpring.TargetPosition * driftRatio;
                    _wheelCollider.suspensionSpring = _suspensionSpring;

                    _forwardFriction.extremumSlip = _carWheelPhysicsSettingsNormal.ForwardFriction.ExtremumSlip * normalRatio + _carWheelPhysicsSettingsDrift.ForwardFriction.ExtremumSlip * driftRatio;
                    _forwardFriction.extremumValue = _carWheelPhysicsSettingsNormal.ForwardFriction.ExtremumValue * normalRatio + _carWheelPhysicsSettingsDrift.ForwardFriction.ExtremumValue * driftRatio;
                    _forwardFriction.asymptoteSlip = _carWheelPhysicsSettingsNormal.ForwardFriction.AsymptoteSlip * normalRatio + _carWheelPhysicsSettingsDrift.ForwardFriction.AsymptoteSlip * driftRatio;
                    _forwardFriction.asymptoteValue = _carWheelPhysicsSettingsNormal.ForwardFriction.AsymptoteValue * normalRatio + _carWheelPhysicsSettingsDrift.ForwardFriction.AsymptoteValue * driftRatio;
                    _forwardFriction.stiffness = _carWheelPhysicsSettingsNormal.ForwardFriction.Stiffness * normalRatio + _carWheelPhysicsSettingsDrift.ForwardFriction.Stiffness * driftRatio;
                    _wheelCollider.forwardFriction = _forwardFriction;

                    _sidewaysFriction.extremumSlip = _carWheelPhysicsSettingsNormal.SidewaysFriction.ExtremumSlip * normalRatio + _carWheelPhysicsSettingsDrift.SidewaysFriction.ExtremumSlip * driftRatio;
                    _sidewaysFriction.extremumValue = _carWheelPhysicsSettingsNormal.SidewaysFriction.ExtremumValue * normalRatio + _carWheelPhysicsSettingsDrift.SidewaysFriction.ExtremumValue * driftRatio;
                    _sidewaysFriction.asymptoteSlip = _carWheelPhysicsSettingsNormal.SidewaysFriction.AsymptoteSlip * normalRatio + _carWheelPhysicsSettingsDrift.SidewaysFriction.AsymptoteSlip * driftRatio;
                    _sidewaysFriction.asymptoteValue = _carWheelPhysicsSettingsNormal.SidewaysFriction.AsymptoteValue * normalRatio + _carWheelPhysicsSettingsDrift.SidewaysFriction.AsymptoteValue * driftRatio;
                    _sidewaysFriction.stiffness = _carWheelPhysicsSettingsNormal.SidewaysFriction.Stiffness * normalRatio + _carWheelPhysicsSettingsDrift.SidewaysFriction.Stiffness * driftRatio;
                    _wheelCollider.sidewaysFriction = _sidewaysFriction;
                }

                _lastHandbrakeAxis = currentHandbrakeAxis;
            }
        }

        private void AnimateWheelMesh()
        {
            if (_wheelMesh)
            {
                Quaternion wheelRotation;
                Vector3 wheelPosition;
                _wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);
                _wheelMesh.position = wheelPosition;
                _wheelMesh.rotation = wheelRotation;
            }
        }
    }
}
