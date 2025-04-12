using NaughtyAttributes;
using UnityEngine;

namespace CarDemo
{
    [RequireComponent(typeof(WheelCollider))]
    public class CarWheelPhysics : MonoBehaviour
    {
        [SerializeField]
        [Required("Car physics controller required")]
        private CarPhysicsController _carPhysicsController = null;

        [SerializeField]
        private Transform _wheelMesh = null;

        private WheelCollider _wheelCollider = null;

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

            if (_carPhysicsController)
            {
                _carPhysicsController.OnCarPhysicsChanged += CarPhysicsController_OnCarPhysicsChanged;
            }

            UpdateWheelParameters(true);
        }

        private void OnDestroy()
        {
            if (_carPhysicsController)
            {
                _carPhysicsController.OnCarPhysicsChanged -= CarPhysicsController_OnCarPhysicsChanged;
            }
        }

        private void Update()
        {
            UpdateWheelParameters();
            AnimateWheelMesh();
        }

        private void CarPhysicsController_OnCarPhysicsChanged()
        {
            UpdateWheelParameters(true);
        }

        private void UpdateWheelParameters(bool forceUpdate = false)
        {
            if (_carPhysicsController)
            {
                float currentHandbrakeAxis = _carPhysicsController.HandbrakeAxis;

                if (forceUpdate || (currentHandbrakeAxis != _lastHandbrakeAxis))
                {
                    CarWheelPhysicsSO carWheelPhysicsSettingsNormal = _carPhysicsController.CarWheelPhysicsSettingsNormal;
                    CarWheelPhysicsSO carWheelPhysicsSettingsDrift = _carPhysicsController.CarWheelPhysicsSettingDrift;

                    if (carWheelPhysicsSettingsNormal && carWheelPhysicsSettingsDrift)
                    {
                        float normalRatio = 1.0f - currentHandbrakeAxis;
                        float driftRatio = currentHandbrakeAxis;

                        _suspensionSpring.spring = carWheelPhysicsSettingsNormal.SuspensionSpring.Spring * normalRatio + carWheelPhysicsSettingsDrift.SuspensionSpring.Spring * driftRatio;
                        _suspensionSpring.damper = carWheelPhysicsSettingsNormal.SuspensionSpring.Damper * normalRatio + carWheelPhysicsSettingsDrift.SuspensionSpring.Damper * driftRatio;
                        _suspensionSpring.targetPosition = carWheelPhysicsSettingsNormal.SuspensionSpring.TargetPosition * normalRatio + carWheelPhysicsSettingsDrift.SuspensionSpring.TargetPosition * driftRatio;
                        _wheelCollider.suspensionSpring = _suspensionSpring;

                        _forwardFriction.extremumSlip = carWheelPhysicsSettingsNormal.ForwardFriction.ExtremumSlip * normalRatio + carWheelPhysicsSettingsDrift.ForwardFriction.ExtremumSlip * driftRatio;
                        _forwardFriction.extremumValue = carWheelPhysicsSettingsNormal.ForwardFriction.ExtremumValue * normalRatio + carWheelPhysicsSettingsDrift.ForwardFriction.ExtremumValue * driftRatio;
                        _forwardFriction.asymptoteSlip = carWheelPhysicsSettingsNormal.ForwardFriction.AsymptoteSlip * normalRatio + carWheelPhysicsSettingsDrift.ForwardFriction.AsymptoteSlip * driftRatio;
                        _forwardFriction.asymptoteValue = carWheelPhysicsSettingsNormal.ForwardFriction.AsymptoteValue * normalRatio + carWheelPhysicsSettingsDrift.ForwardFriction.AsymptoteValue * driftRatio;
                        _forwardFriction.stiffness = carWheelPhysicsSettingsNormal.ForwardFriction.Stiffness * normalRatio + carWheelPhysicsSettingsDrift.ForwardFriction.Stiffness * driftRatio;
                        _wheelCollider.forwardFriction = _forwardFriction;

                        _sidewaysFriction.extremumSlip = carWheelPhysicsSettingsNormal.SidewaysFriction.ExtremumSlip * normalRatio + carWheelPhysicsSettingsDrift.SidewaysFriction.ExtremumSlip * driftRatio;
                        _sidewaysFriction.extremumValue = carWheelPhysicsSettingsNormal.SidewaysFriction.ExtremumValue * normalRatio + carWheelPhysicsSettingsDrift.SidewaysFriction.ExtremumValue * driftRatio;
                        _sidewaysFriction.asymptoteSlip = carWheelPhysicsSettingsNormal.SidewaysFriction.AsymptoteSlip * normalRatio + carWheelPhysicsSettingsDrift.SidewaysFriction.AsymptoteSlip * driftRatio;
                        _sidewaysFriction.asymptoteValue = carWheelPhysicsSettingsNormal.SidewaysFriction.AsymptoteValue * normalRatio + carWheelPhysicsSettingsDrift.SidewaysFriction.AsymptoteValue * driftRatio;
                        _sidewaysFriction.stiffness = carWheelPhysicsSettingsNormal.SidewaysFriction.Stiffness * normalRatio + carWheelPhysicsSettingsDrift.SidewaysFriction.Stiffness * driftRatio;
                        _wheelCollider.sidewaysFriction = _sidewaysFriction;
                    }
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
