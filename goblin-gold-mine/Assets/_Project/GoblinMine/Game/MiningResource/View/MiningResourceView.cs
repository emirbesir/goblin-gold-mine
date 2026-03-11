using System;
using _Project.GoblinMine.Game.Inventory.Model;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using DG.Tweening;
using UnityEngine;
namespace _Project.GoblinMine.Game.MiningResource.View
{
    public class MiningResourceView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private GameObject readyModel;
        [SerializeField] private GameObject depletedModel;

        public MeshRenderer MeshRenderer => meshRenderer;
        public ResourceType ResourceType => resourceType;

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

        public void SetDepleted(bool depleted)
        {
            readyModel.SetActive(!depleted);
            depletedModel.SetActive(depleted);
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

            if (visualConfig.MiningSound != null)
            {
                AudioSource.PlayClipAtPoint(visualConfig.MiningSound, transform.position);
            }
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

    }
}