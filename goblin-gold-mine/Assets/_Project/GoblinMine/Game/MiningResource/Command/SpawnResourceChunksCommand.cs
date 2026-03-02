using System.Threading;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Model;
using _Project.GoblinMine.Game.MiningResource.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.GoblinMine.Game.MiningResource.Command
{
    public class SpawnResourceChunksCommand
    {
        private readonly ResourceChunkView.Pool _chunkPool;
        private readonly ResourceChunkVisualConfiguration _chunkVisualConfiguration;

        public SpawnResourceChunksCommand(
            ResourceChunkView.Pool chunkPool,
            ResourceChunkVisualConfiguration chunkVisualConfiguration)
        {
            _chunkPool = chunkPool;
            _chunkVisualConfiguration = chunkVisualConfiguration;
        }

        public async UniTask Execute(
            MiningResourceModel miningResource,
            Material material,
            Vector3 spawnPosition,
            CancellationToken cancellationToken)
        {
            var remaining = miningResource.ChunkCount;

            for (var i = 0; i < miningResource.ChunkCount; i++)
            {
                var chunkView = _chunkPool.Spawn();
                chunkView.SetPosition(spawnPosition);
                chunkView.SetMaterial(material);

                var groundY = spawnPosition.y;
                var randomOffset = Random.insideUnitSphere * _chunkVisualConfiguration.ScatterRadiusUnits;
                var targetPosition = spawnPosition + randomOffset;
                targetPosition.y = groundY;

                chunkView.OnBounceCompleteAction = () =>
                {
                    _chunkPool.Despawn(chunkView);
                    remaining--;
                };
                chunkView.BounceToPosition(targetPosition, _chunkVisualConfiguration);
            }

            await UniTask.WaitUntil(() => remaining == 0, cancellationToken: cancellationToken);
        }
    }
}