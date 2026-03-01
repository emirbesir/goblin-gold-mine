using Zenject;
using _Project.GoblinMine.Game.Resource.Model;
using _Project.GoblinMine.Game.Resource.Repository;
using _Project.GoblinMine.Game.Resource.Signal;
using _Project.GoblinMine.Game.Resource.View;

namespace _Project.GoblinMine.Game.Resource.Command
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

        public void Execute(ResourceModel resource, ResourceView resourceView)
        {
            resourceView.PlayCollectionEffects();
            
            if (!_resourceRepository.TotalCollected.ContainsKey(resource.ResourceType))
            {
                _resourceRepository.TotalCollected[resource.ResourceType] = 0;
            }

            _resourceRepository.TotalCollected[resource.ResourceType] += resource.CollectionAmount;

            _signalBus.Fire(new ResourceCollectedSignal
            {
                ResourceType = resource.ResourceType,
                CollectionAmount = resource.CollectionAmount
            });
        }
    }
}
