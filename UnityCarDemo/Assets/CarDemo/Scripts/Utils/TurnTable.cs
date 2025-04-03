using UnityEngine;

namespace CarDemo
{
    public class TurnTable : MonoBehaviour
    {
        [SerializeField]
        private float _turningSpeed = 45.0f;

        private void Update()
        {
            transform.Rotate(0.0f, _turningSpeed * Time.deltaTime, 0.0f);
        }
    }
}
