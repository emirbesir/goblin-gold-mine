using _Project.GoblinMine.Game.Gold.Command;
using _Project.GoblinMine.Game.Upgrade.Configuration;
using _Project.GoblinMine.Game.Upgrade.Model;
using _Project.GoblinMine.Game.Upgrade.Repository;
using _Project.GoblinMine.Game.Upgrade.Signal;
using Zenject;

namespace _Project.GoblinMine.Game.Upgrade.Command
{
    public class PurchaseUpgradeCommand
    {
        private readonly UpgradeRepository _upgradeRepository;
        private readonly UpgradeConfiguration _upgradeConfiguration;
        private readonly SpendGoldCommand _spendGoldCommand;
        private readonly SignalBus _signalBus;

        public PurchaseUpgradeCommand(
            UpgradeRepository upgradeRepository,
            UpgradeConfiguration upgradeConfiguration,
            SpendGoldCommand spendGoldCommand,
            SignalBus signalBus)
        {
            _upgradeRepository = upgradeRepository;
            _upgradeConfiguration = upgradeConfiguration;
            _spendGoldCommand = spendGoldCommand;
            _signalBus = signalBus;
        }

        public bool Execute(UpgradeType upgradeType)
        {
            var upgrade = _upgradeRepository.GetUpgradeByType(upgradeType);

            if (!_spendGoldCommand.Execute(upgrade.CurrentCost))
                return false;

            upgrade.Level++;

            var baseCost = GetBaseCost(upgradeType);
            var multiplier = GetCostMultiplier(upgradeType);
            upgrade.CurrentCost = _upgradeConfiguration.GetCostForLevel(baseCost, multiplier, upgrade.Level);

            _signalBus.Fire(new UpgradePurchasedSignal
            {
                UpgradeType = upgradeType,
                NewLevel = upgrade.Level
            });

            return true;
        }

        private int GetBaseCost(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.BuyWorker: return _upgradeConfiguration.BuyWorkerBaseCost;
                case UpgradeType.MiningSpeed: return _upgradeConfiguration.MiningSpeedBaseCost;
                case UpgradeType.MoveSpeed: return _upgradeConfiguration.MoveSpeedBaseCost;
                case UpgradeType.CarryCapacity: return _upgradeConfiguration.CarryCapacityBaseCost;
                default: return 0;
            }
        }

        private float GetCostMultiplier(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.BuyWorker: return _upgradeConfiguration.BuyWorkerCostMultiplier;
                case UpgradeType.MiningSpeed: return _upgradeConfiguration.MiningSpeedCostMultiplier;
                case UpgradeType.MoveSpeed: return _upgradeConfiguration.MoveSpeedCostMultiplier;
                case UpgradeType.CarryCapacity: return _upgradeConfiguration.CarryCapacityCostMultiplier;
                default: return 1f;
            }
        }
    }
}
