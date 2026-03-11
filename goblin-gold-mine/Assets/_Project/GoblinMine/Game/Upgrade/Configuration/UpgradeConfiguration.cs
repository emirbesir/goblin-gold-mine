using UnityEngine;

namespace _Project.GoblinMine.Game.Upgrade.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/Upgrade/UpgradeConfiguration",
        fileName = "UpgradeConfiguration")]
    public class UpgradeConfiguration : ScriptableObject
    {
        [Header("Buy Worker")]
        [SerializeField] private int buyWorkerBaseCost = 50;
        [SerializeField] private float buyWorkerCostMultiplier = 2.5f;

        [Header("Mining Speed")]
        [SerializeField] private int miningSpeedBaseCost = 30;
        [SerializeField] private float miningSpeedCostMultiplier = 2f;
        [SerializeField] private float miningSpeedReductionPerLevel = 0.1f;
        [SerializeField] private float miningSpeedMinIntervalSeconds = 0.2f;

        [Header("Move Speed")]
        [SerializeField] private int moveSpeedBaseCost = 25;
        [SerializeField] private float moveSpeedCostMultiplier = 1.8f;
        [SerializeField] private float moveSpeedIncreasePerLevel = 1f;
        [SerializeField] private float moveSpeedMaxUnitsPerSecond = 15f;

        [Header("Carry Capacity")]
        [SerializeField] private int carryCapacityBaseCost = 20;
        [SerializeField] private float carryCapacityCostMultiplier = 1.6f;
        [SerializeField] private int carryCapacityIncreasePerLevel = 5;

        public int BuyWorkerBaseCost => buyWorkerBaseCost;
        public float BuyWorkerCostMultiplier => buyWorkerCostMultiplier;

        public int MiningSpeedBaseCost => miningSpeedBaseCost;
        public float MiningSpeedCostMultiplier => miningSpeedCostMultiplier;
        public float MiningSpeedReductionPerLevel => miningSpeedReductionPerLevel;
        public float MiningSpeedMinIntervalSeconds => miningSpeedMinIntervalSeconds;

        public int MoveSpeedBaseCost => moveSpeedBaseCost;
        public float MoveSpeedCostMultiplier => moveSpeedCostMultiplier;
        public float MoveSpeedIncreasePerLevel => moveSpeedIncreasePerLevel;
        public float MoveSpeedMaxUnitsPerSecond => moveSpeedMaxUnitsPerSecond;

        public int CarryCapacityBaseCost => carryCapacityBaseCost;
        public float CarryCapacityCostMultiplier => carryCapacityCostMultiplier;
        public int CarryCapacityIncreasePerLevel => carryCapacityIncreasePerLevel;

        public int GetCostForLevel(int baseCost, float multiplier, int level)
        {
            return Mathf.RoundToInt(baseCost * Mathf.Pow(multiplier, level));
        }
    }
}
