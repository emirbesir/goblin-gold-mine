using System;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.MiningResource.View
{
    public class ResourceChunkView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        public MeshRenderer MeshRenderer => meshRenderer;

        public Action OnBounceCompleteAction;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void BounceToPosition(Vector3 target, ResourceChunkVisualConfiguration visualConfig)
        {
            transform.DOJump(
                target,
                jumpPower: visualConfig.JumpPower,
                numJumps: 1,
                duration: visualConfig.JumpDurationSeconds
            )
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(visualConfig.DespawnDelaySeconds, () => 
                    OnBounceCompleteAction?.Invoke());
            });
        }

        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }

        public class Pool : MemoryPool<ResourceChunkView>
        {
            protected override void OnCreated(ResourceChunkView item)
            {
                item.gameObject.SetActive(false);
                base.OnCreated(item);
            }

            protected override void OnSpawned(ResourceChunkView item)
            {
                item.gameObject.SetActive(true);
                base.OnSpawned(item);
            }

            protected override void OnDespawned(ResourceChunkView item)
            {
                item.OnBounceCompleteAction = null;
                item.gameObject.SetActive(false);
                base.OnDespawned(item);
            }
        }
    }
}