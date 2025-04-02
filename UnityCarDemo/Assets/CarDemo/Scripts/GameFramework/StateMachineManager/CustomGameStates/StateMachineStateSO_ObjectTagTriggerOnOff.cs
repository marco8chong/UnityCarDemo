using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "StateMachineStateSO_ObjectTagTriggerOnOff", menuName = "Car Demo/SO/Game State/Object Tag Trigger On Off")]
    public class StateMachineStateSO_ObjectTagTriggerOnOff : StateMachineStateSO
    {
        [SerializeField]
        private bool _callMultipleObjects = true;

        [SerializeField]
        [Required("Object tag SO required")]
        private ObjectTagSO _objectTag;

        [SerializeField]
        private bool _turnedOn = true;

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
                        OnOffObjectTagTrigger(tag);
                    }
                }
                else
                {
                    ObjectTag tag = GameDirector.Instance.ObjectTagManager.GetObjectByTag(_objectTag);

                    if (tag)
                    {
                        OnOffObjectTagTrigger(tag);
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

        private void OnOffObjectTagTrigger(ObjectTag tag)
        {
            ObjectTagTrigger trigger = tag.GetComponent<ObjectTagTrigger>();

            if (trigger)
            {
                trigger.TriggerEnabled = _turnedOn;
            }
        }
    }
}
