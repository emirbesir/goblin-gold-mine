using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;
using UnityEngine;
using _Project.Shared.Initializable;
using _Project.GoblinMine.Game.Haptic.Command;
using _Project.GoblinMine.Game.Inventory.Repository;
using _Project.GoblinMine.Game.Player.Repository;
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
        private readonly RespawnMiningResourceCommand _respawnMiningResourceCommand;
        private readonly PlayerRepository _playerRepository;
        private readonly InventoryRepository _inventoryRepository;
        private readonly TriggerHapticCommand _triggerHapticCommand;

        private CancellationTokenSource _cancellationTokenSource;

        public MiningResourceController(
            MiningResourceConfigurationCollection miningResourceConfigurationCollection,
            MiningResourceRepository miningResourceRepository,
            MiningResourceViewRepository miningResourceViewRepository,
            InitializeMiningResourcesCommand initializeMiningResourcesCommand,
            CollectMiningResourceCommand collectMiningResourceCommand,
            RespawnMiningResourceCommand respawnMiningResourceCommand,
            PlayerRepository playerRepository,
            InventoryRepository inventoryRepository,
            TriggerHapticCommand triggerHapticCommand)
        {
            _miningResourceConfigurationCollection = miningResourceConfigurationCollection;
            _miningResourceRepository = miningResourceRepository;
            _miningResourceViewRepository = miningResourceViewRepository;
            _initializeMiningResourcesCommand = initializeMiningResourcesCommand;
            _collectMiningResourceCommand = collectMiningResourceCommand;
            _respawnMiningResourceCommand = respawnMiningResourceCommand;
            _playerRepository = playerRepository;
            _inventoryRepository = inventoryRepository;
            _triggerHapticCommand = triggerHapticCommand;
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

            if (resource.RemainingDurability <= 0) return;

            var totalCarried = _inventoryRepository.GetTotalCarried();
            var player = _playerRepository.Player;

            if (totalCarried >= player.MaxCarryCapacity) return;

            resource.CollectionTimer += Time.deltaTime;

            var interval = player.MiningIntervalSeconds;
            if (resource.CollectionIntervalSeconds > interval)
                interval = resource.CollectionIntervalSeconds;

            if (resource.CollectionTimer >= interval)
            {
                resource.CollectionTimer -= interval;

                var config = _miningResourceConfigurationCollection.GetConfigurationByType(resource.ResourceType);

                _collectMiningResourceCommand.Execute(resource, resourceView, config, _cancellationTokenSource.Token, autoDeposit: false).Forget();
                _triggerHapticCommand.Execute();

                resource.RemainingDurability--;

                if (resource.RemainingDurability <= 0)
                {
                    resource.CollectionTimer = 0f;
                    resourceView.SetDepleted(true);
                    _respawnMiningResourceCommand.Execute(
                        resource, resourceView, config, _cancellationTokenSource.Token).Forget();
                }
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
