using UnityEngine;

namespace CarDemo
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarAddonTurretBullet : MonoBehaviour
    {
        [SerializeField]
        private float _lifespan = 5.0f;

        private Rigidbody _rigidbody = null;

        private float _timeElapsed = 0.0f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _timeElapsed = 0.0f;
        }

        private void Update()
        {
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed > _lifespan)
            {
                _timeElapsed = 0.0f;

                ResetBullet();
            }
        }

        public void ResetBullet()
        {
            _timeElapsed = 0.0f;

            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            gameObject.SetActive(false);
        }
    }
}
