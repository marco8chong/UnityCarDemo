using UnityEngine;

namespace CarDemo
{
    [RequireComponent(typeof(WheelCollider))]
    public class CarWheelPhysics : MonoBehaviour
    {
        [SerializeField]
        private CarWheelPhysicsSO _carWheelPhysicsSettingsSO;

        [SerializeField]
        private Transform _wheelMesh = null;

        private WheelCollider _wheelCollider = null;

        private bool _initialized = false;

        private void Start()
        {
            InitWheel();
        }

        private void Update()
        {
            if (_initialized)
            {
                AnimateWheelMesh();
            }
        }

        private void InitWheel()
        {
            _wheelCollider = GetComponent<WheelCollider>();

            if (_carWheelPhysicsSettingsSO)
            {
                _initialized = true;

                JointSpring suspensionSpring = new JointSpring();
                suspensionSpring.spring = _carWheelPhysicsSettingsSO.SuspensionSpring.Spring;
                suspensionSpring.damper = _carWheelPhysicsSettingsSO.SuspensionSpring.Damper;
                suspensionSpring.targetPosition = _carWheelPhysicsSettingsSO.SuspensionSpring.TargetPosition;
                _wheelCollider.suspensionSpring = suspensionSpring;

                WheelFrictionCurve forwardFriction = new WheelFrictionCurve();
                forwardFriction.extremumSlip = _carWheelPhysicsSettingsSO.ForwardFriction.ExtremumSlip;
                forwardFriction.extremumValue = _carWheelPhysicsSettingsSO.ForwardFriction.ExtremumValue;
                forwardFriction.asymptoteSlip = _carWheelPhysicsSettingsSO.ForwardFriction.AsymptoteSlip;
                forwardFriction.asymptoteValue = _carWheelPhysicsSettingsSO.ForwardFriction.AsymptoteValue;
                forwardFriction.stiffness = _carWheelPhysicsSettingsSO.ForwardFriction.Stiffness;
                _wheelCollider.forwardFriction = forwardFriction;

                WheelFrictionCurve sidewaysFriction = new WheelFrictionCurve();
                sidewaysFriction.extremumSlip = _carWheelPhysicsSettingsSO.SidewaysFriction.ExtremumSlip;
                sidewaysFriction.extremumValue = _carWheelPhysicsSettingsSO.SidewaysFriction.ExtremumValue;
                sidewaysFriction.asymptoteSlip = _carWheelPhysicsSettingsSO.SidewaysFriction.AsymptoteSlip;
                sidewaysFriction.asymptoteValue = _carWheelPhysicsSettingsSO.SidewaysFriction.AsymptoteValue;
                sidewaysFriction.stiffness = _carWheelPhysicsSettingsSO.SidewaysFriction.Stiffness;
                _wheelCollider.sidewaysFriction = sidewaysFriction;
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
