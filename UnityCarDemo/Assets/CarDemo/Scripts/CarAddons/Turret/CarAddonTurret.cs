using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    public class CarAddonTurret : CarAddonBase
    {
        [SerializeField]
        private CarAddonTurretBullet _bullet = null;

        [SerializeField]
        private Transform _firePosition = null;

        [SerializeField]
        [Range(1.0f, 200.0f)]
        private float _firingForce = 50.0f;

        [SerializeField]
        [Range(10, 100)]
        private int _bulletPoolSize = 30;

        private Queue<CarAddonTurretBullet> _bulletPool = new Queue<CarAddonTurretBullet>();

        private void Start()
        {
            CreateBulletPool();
        }

        private void CreateBulletPool()
        {
            for (int i = 0; i < _bulletPoolSize; i++)
            {
                CarAddonTurretBullet bullet = Instantiate<CarAddonTurretBullet>(_bullet);
                bullet.transform.SetParent(_firePosition.transform, false);
                bullet.ResetBullet();

                _bulletPool.Enqueue(bullet);
            }
        }

        [Button]
        public override void TriggerAddon()
        {
            base.TriggerAddon();

            if (_firePosition && _bullet)
            {
                CarAddonTurretBullet bullet = _bulletPool.Dequeue();

                bullet.ResetBullet();                
                bullet.gameObject.SetActive(true);
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * _firingForce, ForceMode.Impulse);
            
                _bulletPool.Enqueue(bullet);
            }
        }
    }
}
