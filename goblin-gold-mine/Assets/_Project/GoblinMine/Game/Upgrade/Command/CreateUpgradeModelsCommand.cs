using System;
using System.Collections.Generic;
using _Project.GoblinMine.Game.Upgrade.Configuration;
using _Project.GoblinMine.Game.Upgrade.Model;

namespace _Project.GoblinMine.Game.Upgrade.Command
{
    public class CreateUpgradeModelsCommand
    {
        private readonly UpgradeConfiguration _upgradeConfiguration;

        public CreateUpgradeModelsCommand(UpgradeConfiguration upgradeConfiguration)
        {
            _upgradeConfiguration = upgradeConfiguration;
        }

        public List<UpgradeModel> Execute()
        {
            var upgrades = new List<UpgradeModel>();

            foreach (UpgradeType type in Enum.GetValues(typeof(UpgradeType)))
            {
                var baseCost = GetBaseCost(type);
                var multiplier = GetCostMultiplier(type);

                upgrades.Add(new UpgradeModel
                {
                    UpgradeType = type,
                    Level = 0,
                    CurrentCost = _upgradeConfiguration.GetCostForLevel(baseCost, multiplier, 0)
                });
            }

            return upgrades;
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
