using System;
using _Project.GoblinMine.Game.Inventory.Repository;
using _Project.GoblinMine.Game.MiningResource.Signal;
using _Project.Shared.Initializable;
using Zenject;

namespace _Project.GoblinMine.Game.Inventory.Controller
{
    public class InventoryController : IPostInitializable, IDisposable
    {
        private readonly InventoryRepository _inventoryRepository;
        private readonly SignalBus _signalBus;

        public InventoryController(InventoryRepository inventoryRepository, SignalBus signalBus)
        {
            _inventoryRepository = inventoryRepository;
            _signalBus = signalBus;
        }

        public void PostInitialize()
        {
            _signalBus.Subscribe<ResourceCollectedSignal>(OnResourceCollectedReceived);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ResourceCollectedSignal>(OnResourceCollectedReceived);
        }

        private void OnResourceCollectedReceived(ResourceCollectedSignal signal)
        {
            var resource = _inventoryRepository.GetResourceByType(signal.ResourceType);
            resource.Amount += signal.CollectionAmount;
        }
    }
}
