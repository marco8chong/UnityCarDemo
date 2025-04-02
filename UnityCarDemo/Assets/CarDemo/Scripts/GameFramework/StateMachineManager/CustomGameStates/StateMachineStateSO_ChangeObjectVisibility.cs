using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    [CreateAssetMenu(fileName = "StateMachineStateSO_ChangeObjectVisibility", menuName = "Car Demo/SO/Game State/Change Object Visibility")]
    public class StateMachineStateSO_ChangeObjectVisibility : StateMachineStateSO
    {
        [SerializeField]
        private List<ObjectTagSO> _tagsToHide = new List<ObjectTagSO>();

        [SerializeField]
        private List<ObjectTagSO> _tagsToShow = new List<ObjectTagSO>();

        public override void StateStart(StateMachine stateMachine)
        {
            base.StateStart(stateMachine);

            ChangeObjectVisibility();
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

        private void ChangeObjectVisibility()
        {
            // tags to hide
            foreach (ObjectTagSO tag in _tagsToHide)
            {
                List<ObjectTag> objects = GameDirector.Instance.ObjectTagManager.GetObjectsByTag(tag);

                foreach (ObjectTag obj in objects)
                {
                    obj?.gameObject.SetActive(false);
                }
            }

            // tags to show
            foreach (ObjectTagSO tag in _tagsToShow)
            {
                List<ObjectTag> objects = GameDirector.Instance.ObjectTagManager.GetObjectsByTag(tag);

                foreach (ObjectTag obj in objects)
                {
                    obj?.gameObject.SetActive(true);
                }
            }
        }
    }
}
