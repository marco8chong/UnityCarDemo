using UnityEngine;

namespace CarDemo
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField]
        private bool _isPlayer = true;

        public bool IsPlayer
        {
            get
            {
                return _isPlayer;
            }
            set
            {
                _isPlayer = value;
            }
        }

        private void Start()
        {
            if (_isPlayer && GameDirector.Instance && GameDirector.Instance.CameraManager)
            {
                GameDirector.Instance.CameraManager.RegisterCameraTarget(this);
            }
        }

        private void OnDestroy()
        {
            if (GameDirector.Instance && GameDirector.Instance.CameraManager)
            {
                GameDirector.Instance.CameraManager.UnregisterCameraTarget(this);
            }
        }
    }
}
