using GoblinMine.Game.Resource.Model;
using GoblinMine.Game.Resource.Repository;
using GoblinMine.Game.Resource.Signal;
using Zenject;
using UnityEngine;

namespace GoblinMine.Game.Resource.Command
{
    public class CollectResourceCommand
    {
        private readonly ResourceRepository _resourceRepository;
        private readonly SignalBus _signalBus;

        public CollectResourceCommand(
            ResourceRepository resourceRepository,
            SignalBus signalBus)
        {
            _resourceRepository = resourceRepository;
            _signalBus = signalBus;
        }

        public void Execute(ResourceModel resource)
        {
            if (!_resourceRepository.TotalCollected.ContainsKey(resource.ResourceType))
            {
                _resourceRepository.TotalCollected[resource.ResourceType] = 0;
            }

            _resourceRepository.TotalCollected[resource.ResourceType] += resource.Value;

            _signalBus.Fire(new ResourceCollectedSignal
            {
                ResourceType = resource.ResourceType,
                Amount = resource.Value
            });

            Debug.Log($"Collected {resource.Value} of {resource.ResourceType}. Total collected: {_resourceRepository.TotalCollected[resource.ResourceType]}");
        }
    }
}
