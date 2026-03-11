using UnityEngine;

namespace _Project.GoblinMine.Game.Worker.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/Worker/WorkerVisualConfiguration",
        fileName = "WorkerVisualConfiguration")]
    public class WorkerVisualConfiguration : ScriptableObject
    {
        [Header("Sleep Bobbing")]
        [SerializeField] private float sleepBobAmplitudeUnits = 0.15f;
        [SerializeField] private float sleepBobDurationSeconds = 1.2f;

        [Header("Wake Up Punch")]
        [SerializeField] private Vector3 wakeUpPunchScale = new Vector3(0.3f, 0.3f, 0.3f);
        [SerializeField] private float wakeUpPunchDurationSeconds = 0.4f;
        [SerializeField] private int wakeUpPunchVibrato = 10;
        [SerializeField] private float wakeUpPunchElasticity = 1f;

        [Header("Collection Pulse")]
        [SerializeField] private Vector3 collectionPunchScale = new Vector3(0.2f, 0.2f, 0.2f);
        [SerializeField] private float collectionPunchDurationSeconds = 0.3f;
        [SerializeField] private int collectionPunchVibrato = 10;
        [SerializeField] private float collectionPunchElasticity = 1f;

        public float SleepBobAmplitudeUnits => sleepBobAmplitudeUnits;
        public float SleepBobDurationSeconds => sleepBobDurationSeconds;

        public Vector3 WakeUpPunchScale => wakeUpPunchScale;
        public float WakeUpPunchDurationSeconds => wakeUpPunchDurationSeconds;
        public int WakeUpPunchVibrato => wakeUpPunchVibrato;
        public float WakeUpPunchElasticity => wakeUpPunchElasticity;

        public Vector3 CollectionPunchScale => collectionPunchScale;
        public float CollectionPunchDurationSeconds => collectionPunchDurationSeconds;
        public int CollectionPunchVibrato => collectionPunchVibrato;
        public float CollectionPunchElasticity => collectionPunchElasticity;
    }
}
