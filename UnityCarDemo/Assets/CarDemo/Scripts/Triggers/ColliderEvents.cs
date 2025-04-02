using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace CarDemo
{
    [RequireComponent(typeof(Collider))]
    public class ColliderEvents : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _targetLayerMask = new LayerMask();

        [HorizontalLine(color: EColor.Blue)]

        public UnityEvent OnTargetEnter = null;

        [HorizontalLine(color: EColor.Blue)]

        public UnityEvent OnTargetStay = null;

        [SerializeField]
        [Range(0.0f, 30.0f)]
        private float _stayEventDelay = 1.0f;
        [SerializeField]
        private bool _stayEventOneShot = true;

        [HorizontalLine(color: EColor.Blue)]

        public UnityEvent OnTargetExit = null;

        [HorizontalLine(color: EColor.Blue)]

        public UnityEvent OnColliderDisabled = null;

        private float _stayTimeElapsed = 0.0f;
        private bool _stayEventFired = false;

        private void OnDisable()
        {
            OnColliderDisabled?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsTargetObject(other))
            {
                _stayTimeElapsed = 0.0f;
                _stayEventFired = false;

                OnTargetEnter?.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (IsTargetObject(other))
            {
                _stayTimeElapsed += Time.deltaTime;

                if ((_stayTimeElapsed - _stayEventDelay) > 0.0f)
                {
                    if (!(_stayEventOneShot && _stayEventFired))
                    {
                        _stayEventFired = true;
                        OnTargetStay?.Invoke();
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsTargetObject(other))
            {
                _stayTimeElapsed = 0.0f;
                _stayEventFired = false;

                OnTargetExit?.Invoke();
            }
        }

        private bool IsTargetObject(Collider other)
        {
            return (((1 << other.gameObject.layer) & _targetLayerMask) != 0);
        }
    }
}
