using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    public class ObjectTagManager : GameDirectorService
    {
        private Dictionary<ObjectTagSO, List<ObjectTag>> _registeredObjectTags = new Dictionary<ObjectTagSO, List<ObjectTag>>();

        [SerializeField]
        private bool _sortObjectTagByName = true;

        [ShowNativeProperty]
        public int RegisteredTagCount => _registeredObjectTags.Keys.Count;

        public void RegisterObjectTag(ObjectTag objectTag)
        {
            if ((objectTag != null) && (objectTag.ObjectTagSO != null))
            {
                if (_registeredObjectTags.ContainsKey(objectTag.ObjectTagSO))
                {
                    _registeredObjectTags[objectTag.ObjectTagSO].Add(objectTag);

                    if (_sortObjectTagByName)
                    {
                        _registeredObjectTags[objectTag.ObjectTagSO].Sort((a, b) => a.name.CompareTo(b.name));
                    }
                }
                else
                {
                    List<ObjectTag> newList = new List<ObjectTag>();
                    newList.Add(objectTag);

                    _registeredObjectTags.Add(objectTag.ObjectTagSO, newList);
                }
            }
            else
            {
                string objectName = objectTag ? objectTag.name : "null";
                Debug.LogError($"Error when registering object tag object {objectName}.");
            }
        }

        public void UnregisterObjectTag(ObjectTag objectTag)
        {
            if ((objectTag != null) && (objectTag.ObjectTagSO != null))
            {
                if (_registeredObjectTags.ContainsKey(objectTag.ObjectTagSO))
                {
                    _registeredObjectTags[objectTag.ObjectTagSO].Remove(objectTag);

                    if (_registeredObjectTags[objectTag.ObjectTagSO].Count == 0)
                    {
                        _registeredObjectTags.Remove(objectTag.ObjectTagSO);
                    }
                }
            }
            else
            {
                string objectName = objectTag ? objectTag.name : "null";
                Debug.LogError($"Error when unregistering object tag object {objectName}.");
            }
        }

        public ObjectTag GetObjectByTag(ObjectTagSO objectTagSO)
        {
            ObjectTag tag = null;

            if (_registeredObjectTags.ContainsKey(objectTagSO))
            {
                if (_registeredObjectTags[objectTagSO].Count > 0)
                {
                    tag = _registeredObjectTags[objectTagSO][0];
                }
            }

            return tag;
        }

        public List<ObjectTag> GetObjectsByTag(ObjectTagSO objectTagSO)
        {
            List<ObjectTag> objectList = null;

            if (_registeredObjectTags.ContainsKey(objectTagSO))
            {
                objectList = new List<ObjectTag>(_registeredObjectTags[objectTagSO]);
            }
            else
            {
                objectList = new List<ObjectTag>();
            }

            return objectList;
        }
    }
}
