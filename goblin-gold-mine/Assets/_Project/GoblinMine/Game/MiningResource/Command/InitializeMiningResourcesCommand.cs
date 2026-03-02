using System;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Model;
using _Project.GoblinMine.Game.MiningResource.Repository;
using _Project.GoblinMine.Game.MiningResource.View;
using _Project.GoblinMine.Game.Inventory.Command;
using _Project.GoblinMine.Game.Inventory.Repository;
using UnityEngine;

namespace _Project.GoblinMine.Game.MiningResource.Command
{
    public class InitializeMiningResourcesCommand
    {
        private readonly MiningResourceRepository _miningResourceRepository;
        private readonly InventoryRepository _inventoryRepository;
        private readonly MiningResourceViewRepository _miningResourceViewRepository;
        private readonly CreateMiningResourceModelCommand _createMiningResourceModelCommand;
        private readonly CreateResourceModelCommand _createResourceModelCommand;
        private readonly MiningResourceView.Factory _miningResourceViewFactory;

        public InitializeMiningResourcesCommand(
            MiningResourceRepository miningResourceRepository,
            InventoryRepository inventoryRepository,
            MiningResourceViewRepository miningResourceViewRepository,
            CreateMiningResourceModelCommand createMiningResourceModelCommand,
            CreateResourceModelCommand createResourceModelCommand,
            MiningResourceView.Factory miningResourceViewFactory)
        {
            _miningResourceRepository = miningResourceRepository;
            _inventoryRepository = inventoryRepository;
            _miningResourceViewRepository = miningResourceViewRepository;
            _createMiningResourceModelCommand = createMiningResourceModelCommand;
            _createResourceModelCommand = createResourceModelCommand;
            _miningResourceViewFactory = miningResourceViewFactory;
        }

        public void Execute(
            MiningResourceConfigurationCollection configurationCollection,
            Action<MiningResourceView, Collider> onTriggerStay,
            Action<MiningResourceView, Collider> onTriggerExit)
        {
            foreach (var config in configurationCollection.Configurations)
            {
                var miningResource = _createMiningResourceModelCommand.Execute(config);
                _miningResourceRepository.MiningResources.Add(miningResource);

                var resource = _createResourceModelCommand.Execute(config.ResourceType);
                _inventoryRepository.Resources.Add(resource);

                var resourceView = _miningResourceViewFactory.Create();
                resourceView.SetMaterial(config.Material);
                resourceView.Id = miningResource.Id;
                resourceView.OnTriggerStayAction += other => onTriggerStay(resourceView, other);
                resourceView.OnTriggerExitAction += other => onTriggerExit(resourceView, other);

                // For demo purposes, randomly position resources in the scene.
                // In the real game, we will have predefined spawn points.
                resourceView.SetPosition(new Vector3(
                    UnityEngine.Random.Range(-8f, 8f),
                    0f,
                    UnityEngine.Random.Range(-8f, 8f)));

                _miningResourceViewRepository.MiningResourceViews.Add(resourceView);
            }
        }
    }
}
