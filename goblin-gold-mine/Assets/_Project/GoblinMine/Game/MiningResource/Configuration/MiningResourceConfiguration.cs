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
        [Header("Economy")]
        [SerializeField] private int economicValue;
        [Header("Collection")]
        [SerializeField] private int collectionAmount;
        [SerializeField] private float collectionIntervalSeconds;
        [SerializeField] private int chunkCount;

        public ResourceType ResourceType => resourceType;
        public string DisplayName => displayName;
        public Material Material => material;
        public int EconomicValue => economicValue;
        public int CollectionAmount => collectionAmount;
        public float CollectionIntervalSeconds => collectionIntervalSeconds;
        public int ChunkCount => chunkCount;
    }
}