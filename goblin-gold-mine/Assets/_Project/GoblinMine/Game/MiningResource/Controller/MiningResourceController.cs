using System;
using System.Threading;
using Zenject;
using UnityEngine;
using _Project.Shared.Initializable;
using _Project.GoblinMine.Game.Player.View;
using _Project.GoblinMine.Game.MiningResource.Command;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Repository;
using _Project.GoblinMine.Game.MiningResource.View;

namespace _Project.GoblinMine.Game.MiningResource.Controller
{
    public class MiningResourceController : IPreInitializable, IDisposable
    {
        private readonly MiningResourceConfigurationCollection _miningResourceConfigurationCollection;
        private readonly MiningResourceRepository _miningResourceRepository;
        private readonly MiningResourceViewRepository _miningResourceViewRepository;
        private readonly InitializeMiningResourcesCommand _initializeMiningResourcesCommand;
        private readonly CollectMiningResourceCommand _collectMiningResourceCommand;
        private readonly MiningResourceVisualConfiguration _miningResourceVisualConfiguration;

        private CancellationTokenSource _cancellationTokenSource;

        public MiningResourceController(
            MiningResourceConfigurationCollection miningResourceConfigurationCollection,
            MiningResourceRepository miningResourceRepository,
            MiningResourceViewRepository miningResourceViewRepository,
            InitializeMiningResourcesCommand initializeMiningResourcesCommand,
            CollectMiningResourceCommand collectMiningResourceCommand,
            MiningResourceVisualConfiguration miningResourceVisualConfiguration)
        {
            _miningResourceConfigurationCollection = miningResourceConfigurationCollection;
            _miningResourceRepository = miningResourceRepository;
            _miningResourceViewRepository = miningResourceViewRepository;
            _initializeMiningResourcesCommand = initializeMiningResourcesCommand;
            _collectMiningResourceCommand = collectMiningResourceCommand;
            _miningResourceVisualConfiguration = miningResourceVisualConfiguration;
        }

        public void PreInitialize()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _initializeMiningResourcesCommand.Execute(
                _miningResourceConfigurationCollection,
                HandleTriggerStay,
                HandleTriggerExit);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private void HandleTriggerStay(MiningResourceView resourceView, Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _)) return;

            var resource = _miningResourceRepository.GetMiningResourceById(resourceView.Id);

            resource.CollectionTimer += Time.deltaTime;

            if (resource.CollectionTimer >= resource.CollectionIntervalSeconds)
            {
                resource.CollectionTimer -= resource.CollectionIntervalSeconds;
    
                var config = _miningResourceConfigurationCollection.GetConfigurationByType(resource.ResourceType);
                
                _collectMiningResourceCommand.Execute(resource, resourceView, config, _cancellationTokenSource.Token).Forget();
            }
        }

        private void HandleTriggerExit(MiningResourceView resourceView, Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _)) return;

            var resource = _miningResourceRepository.GetMiningResourceById(resourceView.Id);
            resource.CollectionTimer = 0f;
        }
    }
}