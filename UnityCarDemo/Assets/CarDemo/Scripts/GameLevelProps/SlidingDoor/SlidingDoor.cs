using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace CarDemo
{
    public class SlidingDoor : MonoBehaviour
    {
        private enum DoorStatus { Inactive, Opening, Closing }

        [SerializeField]
        private Transform _door = null;

        [SerializeField]
        private Transform _doorOpenPosition = null;

        [SerializeField]
        private Transform _doorClosePosition = null;

        public UnityEvent OnDoorOpen = null;
        public UnityEvent OnDoorClose = null;

        private DoorStatus _doorStatus = DoorStatus.Inactive;

        void Update()
        {
            switch (_doorStatus)
            {
                case DoorStatus.Inactive:
                    break;
                case DoorStatus.Opening:
                    _door.position = Vector3.Lerp(_door.position, _doorOpenPosition.position, Time.deltaTime);

                    if (Vector3.Distance(_door.position, _doorOpenPosition.position) < 0.01f)
                    {
                        _doorStatus = DoorStatus.Inactive;
                    }
                    break;
                case DoorStatus.Closing:
                    _door.position = Vector3.Lerp(_door.position, _doorClosePosition.position, Time.deltaTime);

                    if (Vector3.Distance(_door.position, _doorClosePosition.position) < 0.01f)
                    {
                        _doorStatus = DoorStatus.Inactive;
                    }
                    break;
            }
        }

        [Button]
        public void OpenDoor()
        {
            _doorStatus = DoorStatus.Opening;

            try
            {
                OnDoorOpen?.Invoke();
            }
            catch(System.Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        [Button]
        public void CloseDoor()
        {
            _doorStatus = DoorStatus.Closing;

            try
            {
                OnDoorClose?.Invoke();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
            }
        }
    }
}
