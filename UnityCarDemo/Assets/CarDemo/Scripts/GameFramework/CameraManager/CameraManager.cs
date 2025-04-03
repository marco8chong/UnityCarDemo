using System.Collections.Generic;
using UnityEngine;

namespace CarDemo
{
    public class CameraManager : GameDirectorService
    {
        [SerializeField]
        private bool _followPlayer = true;

        [SerializeField]
        private LayerMask _collisionLayerMask;

        [SerializeField]
        private Vector3 _cameraOffset = new Vector3(0.0f, 1.5f, -7.0f);

        [SerializeField]
        [Range(0.5f, 2.0f)]
        private float _cameraSpeed = 1.0f;

        [SerializeField]
        [Range(0.1f, 5.0f)]
        private float _deadZone = 1.0f;

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

                    float camDist = Vector3.Distance(cameraTargetPosition, _camera.transform.position);
                    float adjustedSpeed = _cameraSpeed * Mathf.Clamp01(camDist / _deadZone);

                    if (CanCameraMove(_camera.transform.position, cameraTargetPosition))
                    {
                        _camera.transform.position = Vector3.Lerp(_camera.transform.position, cameraTargetPosition, Time.deltaTime * 1.5f * adjustedSpeed);
                    }

                    _camera.transform.LookAt(cameraTarget.transform, Vector3.up);
                }
            }
        }

        private bool CanCameraMove(Vector3 currentCamPos, Vector3 targetPos)
        {
            bool result = true;

            Vector3 moveDir = targetPos - currentCamPos;

            if (moveDir.magnitude > 0.0f)
            {
                moveDir.Normalize();

                Ray ray = new Ray();
                ray.origin = currentCamPos;
                ray.direction = moveDir;

                RaycastHit hitInfo = new RaycastHit();

                if (Physics.Raycast(ray, out hitInfo, 0.5f, _collisionLayerMask))
                {
                    if (IsTargetObject(hitInfo.collider))
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        private bool IsTargetObject(Collider other)
        {
            return (((1 << other.gameObject.layer) & _collisionLayerMask) != 0);
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
