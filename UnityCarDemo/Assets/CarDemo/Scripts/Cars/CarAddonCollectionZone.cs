using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace CarDemo
{
    [RequireComponent(typeof(Collider))]
    public class CarAddonCollectionZone : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _targetLayerMask = new LayerMask();

        [SerializeField]
        private CarAddonBase _carAddon = null;

        [SerializeField]
        private bool _installOnceOnly = false;

        public UnityEvent OnTargetEnter = null;
        public UnityEvent OnTargetExit = null;
        public UnityEvent OnAddonInstalled = null;

        private bool _installed = false;

        private void OnTriggerEnter(Collider other)
        {
            if (IsTargetObject(other))
            {
                if (!(_installed && _installOnceOnly))
                {
                    CarAddonController carAddonController = other.GetComponentInParent<CarAddonController>();

                    if (carAddonController && _carAddon)
                    {
                        if (carAddonController.InstallAddon(_carAddon))
                        {
                            _installed = true;
                            OnAddonInstalled?.Invoke();
                        }
                    }
                }

                OnTargetEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsTargetObject(other))
            {
                OnTargetExit?.Invoke();
            }
        }

        private bool IsTargetObject(Collider other)
        {
            return (((1 << other.gameObject.layer) & _targetLayerMask) != 0);
        }

        [Button]
        public void Reset()
        {
            _installed = false;
        }
    }
}
