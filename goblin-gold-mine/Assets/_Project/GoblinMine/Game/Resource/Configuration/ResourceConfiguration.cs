using _Project.GoblinMine.Game.Resource.Model;
using UnityEngine;

namespace _Project.GoblinMine.Game.Resource.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/Resource/ResourceConfiguration",
        fileName = "ResourceConfiguration")]
    public class ResourceConfiguration : ScriptableObject
    {
        [Header("General")]
        [SerializeField] private ResourceType resourceType;
        [Header("Visual")]
        [SerializeField] private string displayName;
        [SerializeField] private Material material;
        [Header("Economy")]
        [SerializeField] private int goldValue;
        [Header("Collection")]
        [SerializeField] private int collectionAmount;
        [SerializeField] private float collectionIntervalSeconds;

        public ResourceType ResourceType => resourceType;
        public string DisplayName => displayName;
        public Material Material => material;
        public int GoldValue => goldValue;
        public int CollectionAmount => collectionAmount;
        public float CollectionIntervalSeconds => collectionIntervalSeconds;
    }
}