using System;
using System.Collections.Generic;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Repository;
using _Project.GoblinMine.Game.MiningResource.View;
using UnityEngine;

namespace _Project.GoblinMine.Game.MiningResource.Command
{
    public class InitializeMiningResourcesCommand
    {
        private readonly MiningResourceRepository _miningResourceRepository;
        private readonly MiningResourceViewRepository _miningResourceViewRepository;
        private readonly CreateMiningResourceModelCommand _createMiningResourceModelCommand;
        private readonly List<MiningResourceView> _sceneViews;

        public InitializeMiningResourcesCommand(
            MiningResourceRepository miningResourceRepository,
            MiningResourceViewRepository miningResourceViewRepository,
            CreateMiningResourceModelCommand createMiningResourceModelCommand,
            List<MiningResourceView> sceneViews)
        {
            _miningResourceRepository = miningResourceRepository;
            _miningResourceViewRepository = miningResourceViewRepository;
            _createMiningResourceModelCommand = createMiningResourceModelCommand;
            _sceneViews = sceneViews;
        }

        public void Execute(
            MiningResourceConfigurationCollection configurationCollection,
            Action<MiningResourceView, Collider> onTriggerStay,
            Action<MiningResourceView, Collider> onTriggerExit)
        {
            foreach (var view in _sceneViews)
            {
                var config = configurationCollection.GetConfigurationByType(view.ResourceType);

                if (config == null)
                {
                    Debug.LogWarning($"No configuration found for ResourceType {view.ResourceType} on {view.name}");
                    continue;
                }

                var miningResource = _createMiningResourceModelCommand.Execute(config);
                _miningResourceRepository.MiningResources.Add(miningResource);

                view.SetMaterial(config.Material);
                view.Id = miningResource.Id;
                view.OnTriggerStayAction += other => onTriggerStay(view, other);
                view.OnTriggerExitAction += other => onTriggerExit(view, other);

                _miningResourceViewRepository.MiningResourceViews.Add(view);
            }
        }
    }
}
