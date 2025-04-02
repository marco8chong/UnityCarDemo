using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "StateMachineStateSO_CallObjectTag", menuName = "Car Demo/SO/Game State/Call Object Tag")]
    public class StateMachineStateSO_CallObjectTag : StateMachineStateSO
    {
        public enum CallMode { None, StartEvent, UpdateEvent, EndEvent }

        [SerializeField]
        private CallMode _callMode = CallMode.StartEvent;

        [SerializeField]
        private bool _callMultipleObjects = true;

        [SerializeField]
        [Required("Object tag SO required")]
        private ObjectTagSO _objectTag;

        public override void StateStart(StateMachine stateMachine)
        {
            base.StateStart(stateMachine);

            if (_objectTag != null)
            {
                if (_callMultipleObjects)
                {
                    List<ObjectTag> objectTags = GameDirector.Instance.ObjectTagManager.GetObjectsByTag(_objectTag);

                    foreach (ObjectTag tag in objectTags)
                    {
                        CallObjectTag(tag);
                    }
                }
                else
                {
                    ObjectTag tag = GameDirector.Instance.ObjectTagManager.GetObjectByTag(_objectTag);

                    if (tag)
                    {
                        CallObjectTag(tag);
                    }
                }
            }
        }

        public override void StateTick()
        {
            base.StateTick();

            EndCurrentState();
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }

        private void CallObjectTag(ObjectTag tag)
        {
            switch (_callMode)
            {
                case CallMode.None:
                    {
                    }
                    break;
                case CallMode.StartEvent:
                    {
                        tag.InvokeStartEvents();
                    }
                    break;
                case CallMode.UpdateEvent:
                    {
                        tag.InvokeUpdateEvents();
                    }
                    break;
                case CallMode.EndEvent:
                    {
                        tag.InvokeEndEvents();
                    }
                    break;
            }
        }
    }
}
