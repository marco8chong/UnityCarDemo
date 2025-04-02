using UnityEngine;

namespace CarDemo
{
    public class HideOnStart : MonoBehaviour
    {
        [SerializeField]
        private bool _hideMeshRenderer = false;

        [SerializeField]
        private bool _hideSkinnedMeshRenderer = false;

        [SerializeField]
        private bool _setInactive = true;

        void Start()
        {
            if (_hideMeshRenderer)
            {
                MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

                if ((meshRenderers != null) && (meshRenderers.Length > 0))
                {
                    foreach (MeshRenderer meshRenderer in meshRenderers)
                    {
                        meshRenderer.enabled = false;
                    }
                }
            }

            if (_hideSkinnedMeshRenderer)
            {
                SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

                if ((skinnedMeshRenderers != null) && (skinnedMeshRenderers.Length > 0))
                {
                    foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
                    {
                        skinnedMeshRenderer.enabled = false;
                    }
                }
            }

            if (_setInactive)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
