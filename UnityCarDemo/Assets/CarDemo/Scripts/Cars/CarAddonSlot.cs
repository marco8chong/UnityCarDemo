using UnityEngine;
using NaughtyAttributes;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CarDemo
{
    public class CarAddonSlot : MonoBehaviour
    {
        public enum AddOnSlotType { Bonnet, Roof, BackHatch, Underside, FrontBumper, RearBumper, LeftFront, LeftRear, RightFront, RightRear }

        [SerializeField]
        private bool _locked = false;

        [SerializeField]
        private AddOnSlotType _slotType = AddOnSlotType.Bonnet;

        [SerializeField]
        private Vector3 _displaySize = Vector3.one * 0.5f;

        [SerializeField]
        private Color _displayColor = Color.yellow;

        [SerializeField]
        private int _displayFontSize = 20;

        private CarAddonBase _installedAddon = null;

        public bool Locked
        {
            get
            {
                return _locked;
            }
            set
            {
                _locked = value;
            }
        }

        public AddOnSlotType SlotType
        {
            get
            {
                return _slotType;
            }
        }

        public bool HasAddon
        {
            get
            {
                return _installedAddon != null;
            }
        }

        public CarAddonBase InstalledAddon
        {
            get
            {
                return _installedAddon;
            }
        }

        public void InstallAddon(CarAddonBase addon)
        {
            if (addon)
            {
                UninstallAddon();

                _installedAddon = Instantiate<CarAddonBase>(addon);
                _installedAddon.transform.SetParent(transform);
                _installedAddon.transform.localPosition = addon.InstallationOffset;
                _installedAddon.transform.localRotation = Quaternion.identity;
            }
        }

        [Button]
        public void UninstallAddon()
        {
            if (_installedAddon && !_locked)
            {
                GameObject.Destroy(_installedAddon.gameObject);
                _installedAddon = null;
            }
        }

        void OnDrawGizmos()
        {
            Matrix4x4 orgMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = _displayColor;
            Gizmos.DrawWireCube(Vector3.zero, _displaySize);
            Gizmos.matrix = orgMatrix;

#if UNITY_EDITOR
            GUIStyle style = new GUIStyle();
            style.normal.textColor = _displayColor;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = _displayFontSize;
            style.fontStyle = FontStyle.Bold;
            Handles.Label(transform.position, $"Addon Slot:\n{_slotType.ToString()}", style);
#endif
        }
    }
}
