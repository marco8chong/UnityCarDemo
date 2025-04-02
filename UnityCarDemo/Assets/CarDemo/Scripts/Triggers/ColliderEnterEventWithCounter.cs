using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CarDemo
{
    [RequireComponent(typeof(Collider))]
    public class ColliderEnterEventWithCounter : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _targetLayerMask = new LayerMask();

        [SerializeField]
        private float _coolDownTime = 0.0f;

        [HorizontalLine(color: EColor.Blue)]

        public List<UnityEvent> _onTargetEnter = new List<UnityEvent>();

        [HorizontalLine(color: EColor.Blue)]

        public UnityEvent OnColliderDisabled = null;

        private int _onEnterEventIndex = 0;
        private float _lastTriggerTime = 0.0f;

        private void OnDisable()
        {
            OnColliderDisabled?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((Time.time - _lastTriggerTime) >= _coolDownTime)
            {
                if (IsTargetObject(other))
                {
                    if ((_onEnterEventIndex < _onTargetEnter.Count) && (_onTargetEnter[_onEnterEventIndex] != null))
                    {
                        _onTargetEnter[_onEnterEventIndex]?.Invoke();
                    }
                    _onEnterEventIndex++;

                    _lastTriggerTime = Time.time;
                }
            }
        }

        private bool IsTargetObject(Collider other)
        {
            return (((1 << other.gameObject.layer) & _targetLayerMask) != 0);
        }
    }
}
