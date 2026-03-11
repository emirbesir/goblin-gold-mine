using System;
using _Project.GoblinMine.Game.Inventory.Command;
using _Project.GoblinMine.Game.Inventory.Model;
using _Project.GoblinMine.Game.Inventory.Repository;
using _Project.GoblinMine.Game.Inventory.View;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Signal;
using _Project.GoblinMine.Game.Player.Repository;
using _Project.GoblinMine.Game.Player.View;
using _Project.Shared.Initializable;
using Zenject;

namespace _Project.GoblinMine.Game.Inventory.Controller
{
    public class InventoryController : IPreInitializable, IPostInitializable, IDisposable
    {
        private readonly InventoryRepository _inventoryRepository;
        private readonly InventoryViewRepository _inventoryViewRepository;
        private readonly ResourceView.Factory _resourceViewFactory;
        private readonly MiningResourceConfigurationCollection _miningResourceConfigurationCollection;
        private readonly CreateResourceModelCommand _createResourceModelCommand;
        private readonly PlayerRepository _playerRepository;
        private readonly CarryCapacityView _carryCapacityView;
        private readonly SignalBus _signalBus;

        public InventoryController(
            InventoryRepository inventoryRepository,
            InventoryViewRepository inventoryViewRepository,
            ResourceView.Factory resourceViewFactory,
            MiningResourceConfigurationCollection miningResourceConfigurationCollection,
            CreateResourceModelCommand createResourceModelCommand,
            PlayerRepository playerRepository,
            CarryCapacityView carryCapacityView,
            SignalBus signalBus)
        {
            _inventoryRepository = inventoryRepository;
            _inventoryViewRepository = inventoryViewRepository;
            _createResourceModelCommand = createResourceModelCommand;
            _miningResourceConfigurationCollection = miningResourceConfigurationCollection;
            _resourceViewFactory = resourceViewFactory;
            _playerRepository = playerRepository;
            _carryCapacityView = carryCapacityView;
            _signalBus = signalBus;
        }

        public void PreInitialize()
        {
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                var resource = _createResourceModelCommand.Execute(type);
                _inventoryRepository.Resources.Add(resource);

                var config = _miningResourceConfigurationCollection.GetConfigurationByType(resource.ResourceType);

                var resourceView = _resourceViewFactory.Create();
                resourceView.Id = resource.Id;
                resourceView.SetSprite(config.Sprite);
                resourceView.SetText(resource.Amount.ToString());
                _inventoryViewRepository.ResourceViews.Add(resourceView);
            }
        }

        public void PostInitialize()
        {
            _signalBus.Subscribe<ResourceCollectedSignal>(OnResourceCollectedReceived);
            UpdateCarryCapacityUI();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ResourceCollectedSignal>(OnResourceCollectedReceived);
        }

        private void OnResourceCollectedReceived(ResourceCollectedSignal signal)
        {
            if (signal.AutoDeposit)
                return;

            var resource = _inventoryRepository.GetResourceByType(signal.ResourceType);
            resource.Amount += signal.CollectionAmount;

            var config = _miningResourceConfigurationCollection.GetConfigurationByType(resource.ResourceType);
            var resourceView = _inventoryViewRepository.GetResourceViewById(resource.Id);
            resourceView.SetText(resource.Amount.ToString());

            UpdateCarryCapacityUI();
        }

        private void UpdateCarryCapacityUI()
        {
            var totalCarried = _inventoryRepository.GetTotalCarried();
            var maxCapacity = _playerRepository.Player.MaxCarryCapacity;
            _carryCapacityView.UpdateCapacity(totalCarried, maxCapacity);
        }
    }
}
