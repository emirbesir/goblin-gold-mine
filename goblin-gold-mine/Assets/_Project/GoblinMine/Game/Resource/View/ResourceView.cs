using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.Resource.View
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        public Guid Id { get; set; }

        public Action<Collider> OnTriggerEnterAction;
        public Action<Collider> OnTriggerStayAction;
        public Action<Collider> OnTriggerExitAction;

        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void PlayCollectionEffects()
        {
            transform.DOPunchScale(
                punch: new Vector3(0.2f, 0.2f, 0.2f),
                duration: 0.3f,
                vibrato: 10,
                elasticity: 1f
            );

            meshRenderer.material.DOColor(Color.white, 0.1f).SetLoops(2, LoopType.Yoyo);
        }


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