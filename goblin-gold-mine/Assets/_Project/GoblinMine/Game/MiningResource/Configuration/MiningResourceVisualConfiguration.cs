using UnityEngine;

namespace _Project.GoblinMine.Game.MiningResource.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/MiningResource/MiningResourceVisualConfiguration",
        fileName = "MiningResourceVisualConfiguration")]
    public class MiningResourceVisualConfiguration : ScriptableObject
    {
        [Header("Punch Effect")]
        [SerializeField] private Vector3 punchScale = new Vector3(0.2f, 0.2f, 0.2f);
        [SerializeField] private float punchDurationSeconds = 0.3f;
        [SerializeField] private int punchVibrato = 10;
        [SerializeField] private float punchElasticity = 1f;

        [Header("Color Flash")]
        [SerializeField] private float colorFlashDurationSeconds = 0.1f;

        public Vector3 PunchScale => punchScale;
        public float PunchDurationSeconds => punchDurationSeconds;
        public int PunchVibrato => punchVibrato;
        public float PunchElasticity => punchElasticity;
        public float ColorFlashDurationSeconds => colorFlashDurationSeconds;
    }
}
