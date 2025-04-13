using UnityEngine;
using UnityEngine.Events;

namespace CarDemo
{
    public class CarAddonBase : MonoBehaviour
    {
        [SerializeField]
        private CarAddonSlot.AddOnSlotType _slotType = CarAddonSlot.AddOnSlotType.Bonnet;

        [SerializeField]
        private bool _turnedOn = true;

        [SerializeField]
        private Vector3 _installationOffset = Vector3.zero;

        public UnityEvent OnTrigger = null; 

        public CarAddonSlot.AddOnSlotType SlotType
        {
            get
            {
                return _slotType;
            }
        }

        public bool TurnedOn
        {
            get
            {
                return _turnedOn;
            }
        }

        public Vector3 InstallationOffset
        {
            get
            {
                return _installationOffset;
            }
        }

        public virtual void TriggerAddon()
        {
            OnTrigger?.Invoke();
        }

        public void TurnOn()
        {
            _turnedOn = true;
        }

        public void TurnOff()
        {
            _turnedOn = false;
        }

        public bool Install(CarAddonSlot slot)
        {
            bool success = false;

            if (slot && !slot.Locked)
            {
                success = true;
                slot.InstallAddon(this);
            }

            return success;
        }

        public void Uninstall()
        {
            CarAddonSlot slot = GetComponentInParent<CarAddonSlot>();

            if (slot)
            {
                slot.UninstallAddon();
            }
        }
    }
}
