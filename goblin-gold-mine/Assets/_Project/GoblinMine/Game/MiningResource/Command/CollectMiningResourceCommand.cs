using System.Threading;
using Cysharp.Threading.Tasks;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Model;
using _Project.GoblinMine.Game.MiningResource.Signal;
using _Project.GoblinMine.Game.MiningResource.View;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.MiningResource.Command
{
    public class CollectMiningResourceCommand
    {
        private readonly SpawnResourceChunksCommand _spawnResourceChunksCommand;
        private readonly MiningResourceVisualConfiguration _miningResourceVisualConfiguration;
        private readonly ResourceChunkVisualConfiguration _chunkVisualConfiguration;
        private readonly SignalBus _signalBus;

        public CollectMiningResourceCommand(
            SpawnResourceChunksCommand spawnResourceChunksCommand,
            MiningResourceVisualConfiguration miningResourceVisualConfiguration,
            ResourceChunkVisualConfiguration chunkVisualConfiguration,
            SignalBus signalBus)
        {
            _spawnResourceChunksCommand = spawnResourceChunksCommand;
            _miningResourceVisualConfiguration = miningResourceVisualConfiguration;
            _chunkVisualConfiguration = chunkVisualConfiguration;
            _signalBus = signalBus;
        }

        public async UniTaskVoid Execute(
            MiningResourceModel resource,
            MiningResourceView resourceView,
            MiningResourceConfiguration configuration,
            CancellationToken cancellationToken)
        {
            resourceView.PlayCollectionEffects(_miningResourceVisualConfiguration);
            await _spawnResourceChunksCommand.Execute(resource, configuration.Material, resourceView.transform.position, cancellationToken);
            
            await UniTask.Delay((int) _chunkVisualConfiguration.DespawnDelaySeconds * 1000, cancellationToken: cancellationToken);
            _signalBus.Fire(new ResourceCollectedSignal
            {
                ResourceType = resource.ResourceType,
                CollectionAmount = resource.CollectionAmount
            });
        }
    }
}
