using _Project.GoblinMine.Game.Depot.Signal;
using _Project.GoblinMine.Game.Gold.Command;
using _Project.GoblinMine.Game.Inventory.Repository;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using Zenject;

namespace _Project.GoblinMine.Game.Depot.Command
{
    public class DepositResourcesCommand
    {
        private readonly InventoryRepository _inventoryRepository;
        private readonly MiningResourceConfigurationCollection _configCollection;
        private readonly EarnGoldCommand _earnGoldCommand;
        private readonly SignalBus _signalBus;

        public DepositResourcesCommand(
            InventoryRepository inventoryRepository,
            MiningResourceConfigurationCollection configCollection,
            EarnGoldCommand earnGoldCommand,
            SignalBus signalBus)
        {
            _inventoryRepository = inventoryRepository;
            _configCollection = configCollection;
            _earnGoldCommand = earnGoldCommand;
            _signalBus = signalBus;
        }

        public int Execute()
        {
            var totalGold = 0;

            foreach (var resource in _inventoryRepository.Resources)
            {
                if (resource.Amount <= 0)
                    continue;

                var config = _configCollection.GetConfigurationByType(resource.ResourceType);
                totalGold += resource.Amount * config.EconomicValue;
                resource.Amount = 0;
            }

            if (totalGold <= 0)
                return 0;

            _earnGoldCommand.Execute(totalGold);

            _signalBus.Fire(new ResourcesDepositedSignal
            {
                GoldEarned = totalGold
            });

            return totalGold;
        }
    }
}
