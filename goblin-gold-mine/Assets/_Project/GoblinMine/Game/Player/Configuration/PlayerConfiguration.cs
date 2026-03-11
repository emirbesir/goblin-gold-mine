using UnityEngine;

namespace _Project.GoblinMine.Game.Player.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/Player/PlayerConfiguration",
        fileName = "PlayerConfiguration")]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] private string displayName;
        [SerializeField] private float moveSpeedUnitsPerSecond;
        [SerializeField] private float miningIntervalSeconds = 0.8f;
        [SerializeField] private int maxCarryCapacity = 20;

        public string DisplayName => displayName;
        public float MoveSpeedUnitsPerSecond => moveSpeedUnitsPerSecond;
        public float MiningIntervalSeconds => miningIntervalSeconds;
        public int MaxCarryCapacity => maxCarryCapacity;
    }
}