using _Project.GoblinMine.Game.Inventory.Model;
using UnityEngine;

namespace _Project.GoblinMine.Game.MiningResource.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/MiningResource/MiningResourceConfiguration",
        fileName = "MiningResourceConfiguration")]
    public class MiningResourceConfiguration : ScriptableObject
    {
        [Header("General")]
        [SerializeField] private ResourceType resourceType;
        [Header("Visual")]
        [SerializeField] private string displayName;
        [SerializeField] private Material material;
        [SerializeField] private Sprite sprite;
        [Header("Economy")]
        [SerializeField] private int economicValue;
        [Header("Collection")]
        [SerializeField] private int collectionAmount;
        [SerializeField] private float collectionIntervalSeconds;
        [SerializeField] private int chunkCount;
        [Header("Durability")]
        [SerializeField] private int durability = 5;
        [SerializeField] private float respawnDelaySeconds = 10f;

        public ResourceType ResourceType => resourceType;
        public string DisplayName => displayName;
        public Material Material => material;
        public Sprite Sprite => sprite;
        public int EconomicValue => economicValue;
        public int CollectionAmount => collectionAmount;
        public float CollectionIntervalSeconds => collectionIntervalSeconds;
        public int ChunkCount => chunkCount;
        public int Durability => durability;
        public float RespawnDelaySeconds => respawnDelaySeconds;
    }
}