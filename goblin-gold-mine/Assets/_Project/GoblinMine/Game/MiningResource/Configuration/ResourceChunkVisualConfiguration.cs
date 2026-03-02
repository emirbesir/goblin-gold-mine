using UnityEngine;

namespace _Project.GoblinMine.Game.MiningResource.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/MiningResource/ResourceChunkVisualConfiguration",
        fileName = "ResourceChunkVisualConfiguration")]
    public class ResourceChunkVisualConfiguration : ScriptableObject
    {
        [Header("Bounce")]
        [SerializeField] private float jumpPower = 2f;
        [SerializeField] private float jumpDurationSeconds = 0.6f;

        [Header("Scatter")]
        [SerializeField] private float scatterRadiusUnits = 1f;

        [Header("Despawn")]
        [SerializeField] private float despawnDelaySeconds = 1f;

        public float JumpPower => jumpPower;
        public float JumpDurationSeconds => jumpDurationSeconds;
        public float ScatterRadiusUnits => scatterRadiusUnits;
        public float DespawnDelaySeconds => despawnDelaySeconds;
    }
}
