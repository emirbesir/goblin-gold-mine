using System;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.MiningResource.View
{
    public class MiningResourceView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        public MeshRenderer MeshRenderer => meshRenderer;
        
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

        public void PlayCollectionEffects(MiningResourceVisualConfiguration visualConfig)
        {
            transform.DOPunchScale(
                punch: visualConfig.PunchScale,
                duration: visualConfig.PunchDurationSeconds,
                vibrato: visualConfig.PunchVibrato,
                elasticity: visualConfig.PunchElasticity
            );

            meshRenderer.material.DOColor(Color.white, visualConfig.ColorFlashDurationSeconds)
                .SetLoops(2, LoopType.Yoyo);
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

        public class Factory : PlaceholderFactory<MiningResourceView>
        {
        }
    }
}