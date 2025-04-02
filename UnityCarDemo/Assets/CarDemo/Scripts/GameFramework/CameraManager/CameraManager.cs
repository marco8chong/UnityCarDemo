using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    public class CameraManager : GameDirectorService
    {
        [SerializeField]
        private bool _followPlayer = true;

        [SerializeField]
        private Vector3 _cameraOffset = new Vector3(0.0f, 2.0f, -7.0f);

        [SerializeField]
        [Range(0.5f, 2.0f)]
        private float _cameraSpeed = 1.0f;

        private Camera _camera;

        private List<CameraTarget> _cameraTargets = new List<CameraTarget>();

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_followPlayer && _camera)
            {
                CameraTarget cameraTarget = GetPlayerCameraTarget();

                if (cameraTarget)
                {
                    Vector3 cameraTargetPosition = cameraTarget.transform.TransformPoint(_cameraOffset);

                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, cameraTargetPosition, Time.deltaTime * 1.5f * _cameraSpeed);
                    _camera.transform.LookAt(cameraTarget.transform, Vector3.up);
                }
            }
        }

        private CameraTarget GetPlayerCameraTarget()
        {
            CameraTarget result = null;

            foreach (CameraTarget target in _cameraTargets)
            {
                if (target.IsPlayer)
                {
                    result = target;
                    break;
                }
            }

            return result;
        }

        public void RegisterCameraTarget(CameraTarget target)
        {
            if (!_cameraTargets.Contains(target))
            {
                _cameraTargets.Add(target);
            }
        }

        public void UnregisterCameraTarget(CameraTarget target)
        {
            if (_cameraTargets.Contains(target))
            {
                _cameraTargets.Remove(target);
            }
        }
    }
}
