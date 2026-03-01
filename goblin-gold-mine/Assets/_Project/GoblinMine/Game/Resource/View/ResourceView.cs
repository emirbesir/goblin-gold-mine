using System;
using UnityEngine;
using Zenject;

namespace GoblinMine.Game.Resource.View
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public Guid Id { get; set; }

        public Action<Collider> OnTriggerEnterAction;
        public Action<Collider> OnTriggerStayAction;
        public Action<Collider> OnTriggerExitAction;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterAction?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerStayAction?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitAction?.Invoke(other);
        }

        public class Factory : PlaceholderFactory<ResourceView>
        {
        }
    }
}