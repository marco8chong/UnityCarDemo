using NaughtyAttributes;
using UnityEngine;

namespace CarDemo
{
    public class CarAddonReset : CarAddonBase
    {
        private Rigidbody _carRigidbody = null;

        private Vector3 _resetPosition = Vector3.zero;
        private Quaternion _resetRotation = Quaternion.identity;

        private void Start()
        {
            CarPhysicsController carPhysicsController = GetComponentInParent<CarPhysicsController>();
        
            if (carPhysicsController)
            {
                _carRigidbody = carPhysicsController.GetComponent<Rigidbody>();
                _resetPosition = _carRigidbody.transform.position;
                _resetRotation = _carRigidbody.transform.rotation;
            }
        }

        [Button]
        public override void TriggerAddon()
        {
            base.TriggerAddon();

            if (_carRigidbody)
            {
                _carRigidbody.linearVelocity = Vector3.zero;
                _carRigidbody.angularVelocity = Vector3.zero;

                _carRigidbody.transform.position = _resetPosition;
                _carRigidbody.transform.rotation = _resetRotation;
            }
        }
    }
}
