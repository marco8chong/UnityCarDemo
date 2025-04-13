using UnityEngine;

namespace CarDemo
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarAddonTurretBullet : MonoBehaviour
    {
        [SerializeField]
        private float _lifespan = 5.0f;

        private float _timeElapsed = 0.0f;

        private void Update()
        {
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed > _lifespan)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
