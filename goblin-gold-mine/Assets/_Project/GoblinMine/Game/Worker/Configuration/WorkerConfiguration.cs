using UnityEngine;

namespace _Project.GoblinMine.Game.Worker.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/Worker/WorkerConfiguration",
        fileName = "WorkerConfiguration")]
    public class WorkerConfiguration : ScriptableObject
    {
        [SerializeField] private int collectionAmount = 1;
        [SerializeField] private float miningIntervalSeconds = 3f;
        [SerializeField] private float awakeDurationSeconds = 30f;
        [SerializeField] private float wakeUpDurationSeconds = 1.5f;
        [SerializeField] private float miningRangeUnits = 3f;

        public int CollectionAmount => collectionAmount;
        public float MiningIntervalSeconds => miningIntervalSeconds;
        public float AwakeDurationSeconds => awakeDurationSeconds;
        public float WakeUpDurationSeconds => wakeUpDurationSeconds;
        public float MiningRangeUnits => miningRangeUnits;
    }
}
