using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace CarDemo
{
    public class ObjectTag : MonoBehaviour
    {
        [SerializeField]
        [Required("Object tag SO required")]
        private ObjectTagSO _objectTagSO;

        [SerializeField]
        private bool _hideWhenStart = false;

        [SerializeField]
        private UnityEvent _startEvent;
        [SerializeField]
        private UnityEvent _updateEvent;
        [SerializeField]
        private UnityEvent _endEvent;

        public ObjectTagSO ObjectTagSO
        {
            get
            {
                return _objectTagSO;
            }
        }

        private void Start()
        {
            GameDirector.Instance.ObjectTagManager.RegisterObjectTag(this);

            if (_hideWhenStart)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            GameDirector.Instance.ObjectTagManager.UnregisterObjectTag(this);
        }

        [Button]
        public void InvokeStartEvents()
        {
            _startEvent?.Invoke();
        }

        [Button]
        public void InvokeUpdateEvents()
        {
            _updateEvent?.Invoke();
        }

        [Button]
        public void InvokeEndEvents()
        {
            _endEvent?.Invoke();
        }
    }
}