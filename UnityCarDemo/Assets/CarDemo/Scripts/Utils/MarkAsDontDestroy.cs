using UnityEngine;

namespace CarDemo
{
    public class MarkAsDontDestroy : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(this);
        }

    }
}
