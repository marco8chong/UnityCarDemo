using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    public class ObjectTagTrigger : MonoBehaviour
    {
        [SerializeField]
        private ObjectTagSO _objectTagSO = null;

        [SerializeField]
        private bool _triggerEnabled = true;

        [SerializeField]
        private bool _startOneShotOnly = false;

        [SerializeField]
        private bool _updateOneShotOnly = false;

        [SerializeField]
        private bool _endOneShotOnly = false;

        private bool _startFired = false;
        private bool _updateFired = false;
        private bool _endFired = false;

        public bool TriggerEnabled
        {
            get
            {
                return _triggerEnabled;
            }
            set
            {
                _triggerEnabled = value;
            }
        }

        [Button]
        public void TriggerStart()
        {
            if (_triggerEnabled && _objectTagSO)
            {
                if (!(_startOneShotOnly && _startFired))
                {
                    _startFired = true;

                    List<ObjectTag> objectTags = GameDirector.Instance.ObjectTagManager.GetObjectsByTag(_objectTagSO);

                    foreach (ObjectTag objTag in objectTags)
                    {
                        objTag.InvokeStartEvents();
                    }
                }
            }
        }

        [Button]
        public void TriggerUpdate()
        {
            if (_triggerEnabled && _objectTagSO)
            {
                if (!(_updateOneShotOnly && _updateFired))
                {
                    _updateFired = true;

                    List<ObjectTag> objectTags = GameDirector.Instance.ObjectTagManager.GetObjectsByTag(_objectTagSO);

                    foreach (ObjectTag objTag in objectTags)
                    {
                        objTag.InvokeUpdateEvents();
                    }
                }
            }
        }

        [Button]
        public void TriggerEnd()
        {
            if (_triggerEnabled && _objectTagSO)
            {
                if (!(_endOneShotOnly && _endFired))
                {
                    _endFired = true;

                    List<ObjectTag> objectTags = GameDirector.Instance.ObjectTagManager.GetObjectsByTag(_objectTagSO);

                    foreach (ObjectTag objTag in objectTags)
                    {
                        objTag.InvokeEndEvents();
                    }
                }
            }
        }
    }
}
