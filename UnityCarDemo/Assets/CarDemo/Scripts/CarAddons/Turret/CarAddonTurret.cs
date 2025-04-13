using NaughtyAttributes;
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

        [Button]
        public override void TriggerAddon()
        {
            base.TriggerAddon();

            if (_firePosition && _bullet)
            {
                CarAddonTurretBullet bullet = Instantiate<CarAddonTurretBullet>(_bullet);
                bullet.transform.SetParent(_firePosition.transform, false);
                bullet.transform.position = _firePosition.transform.position;
                bullet.transform.rotation = _firePosition.transform.rotation;

                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * _firingForce, ForceMode.Impulse);
            }
        }
    }
}
