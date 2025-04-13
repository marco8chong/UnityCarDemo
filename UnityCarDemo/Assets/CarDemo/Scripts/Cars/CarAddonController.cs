using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    public class CarAddonController : MonoBehaviour
    {
        [SerializeField]
        private List<CarAddonSlot> _addonSlots = new List<CarAddonSlot>();

        public bool InstallAddon(CarAddonBase addon)
        {
            bool success = false;

            if (addon)
            {
                foreach (CarAddonSlot slot in _addonSlots)
                {
                    if (slot && !slot.Locked && (slot.SlotType == addon.SlotType))
                    {
                        slot.InstallAddon(addon);
                        success = true;

                        break;
                    }
                }
            }

            return success;
        }

        public void UninstallAddon(CarAddonSlot.AddOnSlotType slotType)
        {
            foreach (CarAddonSlot slot in _addonSlots)
            {
                if (slot && !slot.Locked && (slot.SlotType == slotType))
                {
                    slot.UninstallAddon();
                }
            }
        }

        public void UninstallAllAddons()
        {
            foreach (CarAddonSlot slot in _addonSlots)
            {
                if (slot && !slot.Locked)
                {
                    slot.UninstallAddon();
                }
            }
        }

        [Button]
        public void TriggerAllAddons()
        {
            foreach (CarAddonSlot slot in _addonSlots)
            {
                if (slot && slot.HasAddon)
                {
                    slot.InstalledAddon.TriggerAddon();
                }
            }
        }

        public void TriggerAddon(CarAddonSlot.AddOnSlotType slotType)
        {
            foreach (CarAddonSlot slot in _addonSlots)
            {
                if (slot && (slot.SlotType == slotType) && slot.HasAddon)
                {
                    slot.InstalledAddon.TriggerAddon();
                }
            }
        }
    }
}
